using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _velocity = 30f;
    private float _horizontalMovement;

    private bool _isJumping;

    private Controller2D _control;

    private void Awake()
    {
        _control = GetComponent<Controller2D>();        
    }

    private void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal") * _velocity;
        if (Input.GetButtonDown("Jump"))
        {
            _isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        _control.Movement(_horizontalMovement * Time.fixedDeltaTime, _isJumping);
        _isJumping = false;
    }
}
