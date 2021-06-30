using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float _moveSpeed = 10f;

    private GameObject _aimPlayer;
    private Rigidbody2D _rbFireball;
    private Vector2 _aimPosition;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _rbFireball = GetComponent<Rigidbody2D>();
        _aimPlayer = GameObject.Find("SpitfireGun");
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }


    private void OnEnable()
    {
        _aimPosition = _aimPlayer.transform.up;
    }


    void FixedUpdate()
    {
        transform.rotation = new Quaternion(0, 0, _aimPlayer.transform.rotation.z, 0);
        _rbFireball.MovePosition(_rbFireball.position + _aimPosition * _moveSpeed * Time.fixedDeltaTime);
        Destroy(gameObject, 6f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Firepit"))
        {
            var firepitPos = new Vector2(collision.transform.position.x, collision.transform.position.y);
            var playerPos = _playerMovement.transform.position;

            _playerMovement.TradePositionPlayer(firepitPos);
            collision.transform.position = new Vector2(playerPos.x, playerPos.y);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Waterbarrier"))
        {
            Destroy(gameObject);
        }
    }
}
