using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float _moveSpeed = 10f;
    private Rigidbody2D _rbFireball;

    private void Awake()
    {
        _rbFireball = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movement = Vector2.up;
        _rbFireball.MovePosition(_rbFireball.position + movement * _moveSpeed * Time.fixedDeltaTime);
        Destroy(gameObject, 6f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Firepit"))
        {
            Destroy(gameObject);
        }
    }
}
