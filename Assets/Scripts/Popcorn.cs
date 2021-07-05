using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : MonoBehaviour
{
    private Animator _popcornAnim;

    private void Awake()
    {
        _popcornAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _popcornAnim.SetTrigger("Bursted");
            //Reproduz o som
            Destroy(gameObject, 3f);
        }
    }
}
