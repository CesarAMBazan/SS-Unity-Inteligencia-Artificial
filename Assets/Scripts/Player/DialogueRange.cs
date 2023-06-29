using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueRange : MonoBehaviour
{
    [SerializeField] private LayerMask dialogueMask;

    [SerializeField] private Camera PlayerCamera;

    [SerializeField] private Transform dialogueTarget;
    [SerializeField] private GameObject talkBox;
    [Space] [SerializeField] private float dialogueRange;
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
            talkBox.SetActive(true);
        }else talkBox.SetActive(false);
        
        if (firstPersonController.isInteracting == false)
        {
            Ray cameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            
            Debug.DrawRay(cameraRay.origin, cameraRay.direction * dialogueRange, Color.red);
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, dialogueRange))
            {
                currentObject = hitInfo.rigidbody;
                firstPersonController.Interactable = currentObject.GetComponent<DialogueActivator>();
            }
            else
            {
                if(currentObject){
                    currentObject.GetComponent<DialogueActivator>().StopInteracting();
                }
                currentObject = null;
                firstPersonController.Interactable = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Ray cameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Gizmos.color = Color.red;
        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, dialogueRange))
        {
            Gizmos.DrawSphere(hitInfo.point, 0.1f);
        }
        
    }
}
