using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class continuosMovementPhysics : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private float _turnSpeed = 60f;
    [SerializeField]
    private InputActionProperty _moveInputSource;
    [SerializeField]
    private InputActionProperty _turnInputSource;
    /// <summary>
    /// Jumping Variables
    /// </summary>
    [SerializeField]
    private InputActionProperty _jumpInputSource;
    float _jumpVelocity;
    [SerializeField]
    private float _jumpHeight = 1.5f;

    public bool onlyMoveWhenGrounded = false;

    [SerializeField]
    private Rigidbody _rigidBody;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private Transform _directionSource;
    [SerializeField]
    private Transform _turnSource;
    [SerializeField]
    private CapsuleCollider _bodyCollider;
    
    private Vector2 inputMoveAxis;
    private float inputTurnAxis;

    bool jumpInput;
    bool isGrounded;

    private void Start()
    {
        _jumpVelocity = Mathf.Sqrt(2 * -Physics.gravity.y * _jumpHeight); // take value from gravity and it could go in update to read every moment if we have height dinamic
    }
    void Update()
    {
        inputMoveAxis = _moveInputSource.action.ReadValue<Vector2>();
        inputTurnAxis = _turnInputSource.action.ReadValue<Vector2>().x;

        jumpInput = _jumpInputSource.action.WasPressedThisFrame();
        if(jumpInput && isGrounded)
        {
            _rigidBody.velocity += Vector3.up * _jumpVelocity;
        }
    }
    private void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();
       
        if (!onlyMoveWhenGrounded || (onlyMoveWhenGrounded && isGrounded))
        {
            //Debug.Log("enter");
            Quaternion yaw = Quaternion.Euler(0, _directionSource.eulerAngles.y, 0);
            Vector3 direction = yaw * new Vector3(inputMoveAxis.x, 0, inputMoveAxis.y);

            Vector3 targetMovePosition = _rigidBody.position + direction * Time.fixedDeltaTime * _speed;

            Vector3 axis = Vector3.up;
            float angle = _turnSpeed * Time.fixedDeltaTime * inputTurnAxis;
            
            Quaternion q = Quaternion.AngleAxis(angle, axis);
            _rigidBody.MoveRotation(_rigidBody.rotation * q);

            Vector3 newPosition = q * (targetMovePosition - _turnSource.position) + _turnSource.position;
               
            _rigidBody.MovePosition(newPosition);
        }
        
    }
    public bool CheckIfGrounded()
    {
        Vector3 start = _bodyCollider.transform.TransformPoint(_bodyCollider.center);
        float rayLength = _bodyCollider.height / 2 - _bodyCollider.radius + 0.1f;
       
        bool hasHit = Physics.SphereCast(start, _bodyCollider.radius, Vector3.down, out RaycastHit hitInfo, rayLength, _groundLayer);
        return hasHit;
    }
}
