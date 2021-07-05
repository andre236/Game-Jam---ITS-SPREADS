using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private float _smoothFactor;

    private Transform _target;
    [SerializeField]
    private Vector3 _offset;

    void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 targetPosition = _target.position + _offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, _smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

}
