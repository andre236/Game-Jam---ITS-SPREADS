using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeedCamera = 3f;

    private GameObject _chasedGO;
    [SerializeField]
    private Vector2 _followOffset;
    private Vector2 _threshold;
    private Rigidbody2D _chasedRB;


    void Awake()
    {
        _chasedGO = GameObject.FindGameObjectWithTag("Player");
        _threshold = CalculatedThreshold();
        _chasedRB = _chasedGO.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 follow = _chasedGO.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= _threshold.x)
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= _threshold.y)
        {
            newPosition.y = follow.y;
        }
        float moveSpeed = _chasedRB.velocity.magnitude > _moveSpeedCamera ? _chasedRB.velocity.magnitude : _moveSpeedCamera;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    private Vector3 CalculatedThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= _followOffset.x;
        t.y -= _followOffset.y;
        return t;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = CalculatedThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
