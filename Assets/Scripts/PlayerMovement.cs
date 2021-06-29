using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _velocity = 48f;
    private float _horizontalMovement;

    private bool _isJumping;

    private Animator _playerAnim;
    private Controller2D _control;

    private void Awake()
    {
        _control = GetComponent<Controller2D>();
        _playerAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        CommandsPlayer();
    }

    private void FixedUpdate()
    {
        _control.Movement(_horizontalMovement * Time.fixedDeltaTime, _isJumping);
        _isJumping = false;
    }

    public void TradePositionPlayer(Vector2 firepitPos)
    {
        transform.position = new Vector2(firepitPos.x, firepitPos.y);
    }

    void CommandsPlayer()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal") * _velocity;
        if (Input.GetButtonDown("Jump"))
        {
            _isJumping = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _control.Shot();
        }

        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyUp(KeyCode.D))
        {
            _playerAnim.SetBool("IsRunning_L", true);
            _playerAnim.SetBool("IsRunning_R", false);
        }
        else if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyUp(KeyCode.A))
        {
            _playerAnim.SetBool("IsRunning_R", true);
            _playerAnim.SetBool("IsRunning_L", false);
        }
        else
        {
            _playerAnim.SetBool("IsRunning_L", false);
            _playerAnim.SetBool("IsRunning_R", false);
        }

    }

}
