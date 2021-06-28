using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Controller2D : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce = 800f;
    private float _smoothMovement = 0.05f;
    private float _radiusToGround = 0.3f;
    private float _cooldownToShot = 2f;

    private bool _airControl = true;
    private bool _isOnGround;
    private bool _canShot = true;

    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private Transform _footPosition;
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private GameObject _fireballPrefab;
    private GameObject _aimToShotGO;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _aimToShotGO = GameObject.Find("SpitfireGun");
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

    public void Shot()
    {
        if (_canShot)
        {
            _canShot = false;
            Instantiate(_fireballPrefab, _aimToShotGO.transform.position, Quaternion.identity);
            StartCoroutine("CooldownToShot");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            GameManager.Instance.RestartLevel();
        }
    }

    IEnumerator CooldownToShot()
    {
        yield return new WaitForSeconds(_cooldownToShot);
        _canShot = true;
    }
}
