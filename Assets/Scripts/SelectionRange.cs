using System;
using UnityEngine;


public class SelectionRange : MonoBehaviour
{
    [SerializeField] private LayerMask selectionMask;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform selectionTarget;
    [SerializeField] private GameObject selectionBox;
    [SerializeField] private float selectionRange;
    private FirstPersonController firstPersonController;
    private Rigidbody currentObject;

    private void Start()
    {
        firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if (currentObject && firstPersonController.isInteracting == false)
        {
            selectionBox.SetActive(true);
        }else selectionBox.SetActive(false);

        if (firstPersonController.isInteracting == false)
        {
            Ray cameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, selectionRange, selectionMask))
            {
                currentObject = hitInfo.rigidbody;
                firstPersonController.SelectionInt = currentObject.GetComponent<SelectionActivation>();
            }
            else
            {
                currentObject = null;
                firstPersonController.SelectionInt = null;
            }
        }
    }
}