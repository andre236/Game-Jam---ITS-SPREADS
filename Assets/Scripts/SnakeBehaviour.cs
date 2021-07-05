using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    private int _movementRight = 1;

    private float _moveSpeed = 20f;
    private float _horizontalMovement;
    [SerializeField]
    private float _distanceRaycast;

    [SerializeField]
    private Vector2 _offsetRayRight;
    [SerializeField]
    private Vector2 _offsetRayLeft;
    private LayerMask _permissionsLayers;
    private Controller2D _control;


    private void Awake()
    {
        _control = GetComponent<Controller2D>();
    }

    private void Update()
    {
        _horizontalMovement = _movementRight * _moveSpeed;

        RaycastHit2D detectRayRight = Physics2D.Raycast(new Vector2(transform.position.x + _offsetRayRight.x, transform.position.y + _offsetRayRight.y), Vector2.down, _permissionsLayers);
  
        RaycastHit2D detectRayLeft = Physics2D.Raycast(new Vector2(transform.position.x + _offsetRayLeft.x, transform.position.y + _offsetRayLeft.y), Vector2.down, _permissionsLayers);
       
        if (detectRayRight.collider == null)
        {
            _movementRight = -1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        
        if(detectRayLeft.collider == null)
        {
            _movementRight = 1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _control.Movement(_horizontalMovement * Time.fixedDeltaTime, false);
        
    }




}
