using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPickup : MonoBehaviour
{
    [SerializeField] private LayerMask PickupMask;

    [SerializeField] private Camera PlayerCamera;

    [SerializeField] private Transform PickupTarget;

    [Space] [SerializeField] private float PickupRange;

    private FirstPersonController firstPersonController;
    private Rigidbody currentObject;

    private void Start()
    {
        firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && firstPersonController.isInteracting == false)
        {
            if (currentObject)
            {
                currentObject.useGravity = true;
                Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider>(),
                    currentObject.GetComponent<Collider>(), false);
                currentObject = null;
                return;
            }
            Ray cameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, PickupRange, PickupMask))
            {
                currentObject = hitInfo.rigidbody;
                currentObject.useGravity = false;
                Physics.IgnoreCollision(GameObject.Find("Player").GetComponent<Collider>(), currentObject.GetComponent<Collider>(), true);
            }
        }
    }

    void FixedUpdate()
    {
        if (currentObject)
        {
            Vector3 directionToPoint = PickupTarget.position - currentObject.position;
            float distanceToPoint = directionToPoint.magnitude;

            currentObject.velocity = directionToPoint * (12f * distanceToPoint);
        }
    }
}
