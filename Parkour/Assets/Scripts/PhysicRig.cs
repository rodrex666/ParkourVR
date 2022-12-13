using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRig : MonoBehaviour
{
    [SerializeField]
    private Transform _playerHead;
    [SerializeField]
    private Transform _leftController;
    [SerializeField]
    private Transform _rightController;

    [SerializeField]
    private ConfigurableJoint _headJoint;
    [SerializeField]
    private ConfigurableJoint _leftHandJoint;
    [SerializeField]
    private ConfigurableJoint _rightHandJoint;

    [SerializeField]
    private CapsuleCollider _bodyCollider;

    [SerializeField]
    private float _bodyHeightMin = 0.5f;
    [SerializeField]
    private float _bodyHeightMax = 2f;

  
    private void FixedUpdate()
    {
        _bodyCollider.height = Mathf.Clamp(_playerHead.localPosition.y, _bodyHeightMin, _bodyHeightMax);
        _bodyCollider.center = new Vector3(_playerHead.localPosition.x, _bodyCollider.height / 2, _playerHead.localPosition.z);
    
        _leftHandJoint.targetPosition = _leftController.localPosition;
        _leftHandJoint.targetRotation = _leftController.localRotation;

        _rightHandJoint.targetPosition = _rightController.localPosition;
        _rightHandJoint.targetRotation = _rightController.localRotation;

        _headJoint.targetPosition = _playerHead.localPosition;
    }
}
