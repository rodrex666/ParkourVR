using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private Transform _p1;
    [SerializeField]
    private Transform _p2;
    [SerializeField]
    private Rigidbody _rb;

    private Vector3 _targetPosition;

    void Start()
    {
        _targetPosition = _p1.position;
    }


    private void FixedUpdate()
    {
        Vector3 direction = (_targetPosition - _rb.position).normalized;
        _rb.MovePosition(_rb.position + _speed * direction * Time.fixedDeltaTime);

        if(Vector3.Distance(_rb.position, _targetPosition) < 0.05f)
        {
            if (_targetPosition == _p1.position)
                _targetPosition = _p2.position;
            else
                _targetPosition = _p1.position;
        }
    }
}
