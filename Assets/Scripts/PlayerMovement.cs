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
        var ArrowUpOrWbutton = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        _horizontalMovement = Input.GetAxisRaw("Horizontal") * _velocity;
        _playerAnim.SetBool("IsJumping", !_control.IsOnGround);

        if (Input.GetButtonDown("Jump") || ArrowUpOrWbutton)
        {
            _isJumping = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _control.Shot();
        }

        if (_horizontalMovement < 0)
        {
            _playerAnim.SetBool("IsRunning", true);
            GetComponent<SpriteRenderer>().flipX = false;
           
        } 
        else if(_horizontalMovement > 0)
        {
            _playerAnim.SetBool("IsRunning", true);
            GetComponent<SpriteRenderer>().flipX = true;
        } 
        else if(_horizontalMovement == 0)
        {
            _playerAnim.SetBool("IsRunning", false);
        }
       
      

    }

}
