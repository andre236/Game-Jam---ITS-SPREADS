using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Controller2D : MonoBehaviour
{
    [SerializeField]
    private float _jumpForce = 700f;
    private float _smoothMovement = 0.05f;
    private float _radiusToGround = 0.3f;
    private float _cooldownToShot = 2f;

    private bool _airControl = true;

    private bool _canShot = true;

    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private Transform _footPosition;
    private Rigidbody2D _playerRB;

    [SerializeField]
    private GameObject _fireballPrefab;
    private GameObject _aimToShotGO;

    public bool IsOnGround { get; private set; }

    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _aimToShotGO = GameObject.Find("SpitfireGun");
    }

    private void FixedUpdate()
    {
        IsOnGround = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_footPosition.position, _radiusToGround, _groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                IsOnGround = true;
        }

    }

    public void Movement(float amountMovement, bool isJumping)
    {
        if (IsOnGround || _airControl)
        {
            ApplyMovement(amountMovement);
        }

        if (IsOnGround && isJumping)
        {
            IsOnGround = false;
            _playerRB.AddForce(new Vector2(_playerRB.position.x, _jumpForce));
        }
    }

    private void ApplyMovement(float amountMovement)
    {
        var velocityPlayer = new Vector2(amountMovement * 10, _playerRB.velocity.y);

        Vector2 velocity = Vector2.zero;
        _playerRB.velocity = Vector2.SmoothDamp(_playerRB.velocity, velocityPlayer, ref velocity, _smoothMovement);
    }

    public void Shot()
    {
        if (_canShot)
        {
            _canShot = false;
            Instantiate(_fireballPrefab, _aimToShotGO.transform.position, _aimToShotGO.transform.rotation);
            StartCoroutine("CooldownToShot");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death") && this.gameObject.CompareTag("Player"))
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
