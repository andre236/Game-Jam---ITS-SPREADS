using UnityEngine;

public class DropWater : MonoBehaviour
{
    private Rigidbody2D _dropWaterRB;

    private void Awake()
    {
        _dropWaterRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
