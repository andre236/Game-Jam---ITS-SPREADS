using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private bool _isMovingUp;
    [SerializeField]
    private bool _isMovingRight;
    [SerializeField]
    private bool _isPlataformVertical;

    [Header("Obs: O A precisa ter valor > que B para Ambos.")]
    [SerializeField]
    private Vector2 _limitA, _limitB;
    private Rigidbody2D _plataformRB;

    private void Awake()
    {
        _plataformRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if(transform.position.y >= _limitA.y && _isPlataformVertical)
        {
            _isMovingUp = false;
        }

        if (transform.position.y <= _limitB.y && _isPlataformVertical)
        {
            _isMovingUp = true;
        }

        if(transform.position.x >= _limitA.x && !_isPlataformVertical)
        {
            _isMovingRight = false;
        }

        if(transform.position.x <= _limitB.x && !_isPlataformVertical)
        {
            _isMovingRight = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isMovingUp && _isPlataformVertical)
        {
            _plataformRB.transform.position = Vector2.MoveTowards(transform.position, _limitA, _moveSpeed * Time.fixedDeltaTime);
        }

        if (!_isMovingUp && _isPlataformVertical)
        {
            _plataformRB.transform.position = Vector2.MoveTowards(transform.position, _limitB, _moveSpeed * Time.fixedDeltaTime);
        }

        if(_isMovingRight && !_isPlataformVertical)
        {
            _plataformRB.transform.position = Vector2.MoveTowards(transform.position, _limitA, _moveSpeed * Time.fixedDeltaTime);
        }

        if(!_isMovingRight && !_isPlataformVertical)
        {
            _plataformRB.transform.position = Vector2.MoveTowards(transform.position, _limitB, _moveSpeed * Time.fixedDeltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
