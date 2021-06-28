using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Controller2D : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce = 200f;
    private float _smoothMovement = 0.05f;
    private float _radiusToGround = 0.3f;

    private bool _airControl = true;
    private bool _isOnGround;

    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private Transform _footPosition;
    private Rigidbody2D _rigidbody2D;

    private GameManager _gameManager;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        _isOnGround = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_footPosition.position, _radiusToGround, _groundLayer);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                _isOnGround = true;
        }
    }

    public void Movement(float amountMovement, bool isJumping)
    {
        if(_isOnGround || _airControl)
        {
            ApplyMovement(amountMovement);
        }

        if(_isOnGround && isJumping)
        {
            _isOnGround = false;
            _rigidbody2D.AddForce(new Vector2(_rigidbody2D.position.x, _jumpForce));
        }
    }

    private void ApplyMovement(float amountMovement)
    {
        var velocityPlayer = new Vector2(amountMovement * 10, _rigidbody2D.velocity.y);

        Vector2 velocity = Vector2.zero;
        _rigidbody2D.velocity = Vector2.SmoothDamp(_rigidbody2D.velocity, velocityPlayer, ref velocity, _smoothMovement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.Instance.RestartLevel();
        }
    }
}
