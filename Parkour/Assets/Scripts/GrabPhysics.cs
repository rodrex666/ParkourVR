using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabPhysics : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty _grabInputSource;
    [SerializeField]
    private float _radius = 0.1F;
    [SerializeField]
    private LayerMask _grabLayer;

    private FixedJoint fixedJoint;
    private bool isGrabbing = false;
    private void FixedUpdate()
    {
        bool isGrabButtonPressed = _grabInputSource.action.ReadValue<float>() > 0.1F;

        if (isGrabButtonPressed && !isGrabbing)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, _radius, _grabLayer, QueryTriggerInteraction.Ignore);

            if(nearbyColliders.Length > 0)
            {
                Rigidbody nearbyRigidbody = nearbyColliders[0].attachedRigidbody;

                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.autoConfigureConnectedAnchor = false;

                if (nearbyRigidbody)
                {
                    fixedJoint.connectedBody = nearbyRigidbody;
                    fixedJoint.connectedAnchor = nearbyRigidbody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    fixedJoint.connectedAnchor = transform.position; //for walls or somethin inmovil
                }
                isGrabbing = true;
            }
        }else if(!isGrabButtonPressed && isGrabbing)
        {
            isGrabbing = false;
            if (fixedJoint)
            {
                Destroy(fixedJoint);   
            }
        }
    }
}
