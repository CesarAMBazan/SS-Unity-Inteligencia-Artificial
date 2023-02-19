using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractionRange : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform DialogueTarget;
    [Space]
    [SerializeField] private GameObject DialogueBox;

    [SerializeField] private GameObject ExplanationBox;
    [Space] [SerializeField] private float DialogueRange;
    private FirstPersonController _firstPersonController;
    private Rigidbody _currentObject;
    private LayerMask _dialogueLayer;
    private LayerMask _explanationLayer;

    private void Start()
    {
        _firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
        _dialogueLayer = LayerMask.NameToLayer("NPC");
        _explanationLayer = LayerMask.NameToLayer("Exp");
    }

    private void Update()
    {
        if (_currentObject && _firstPersonController.isInteracting == false)
        {
            if (_currentObject.transform.gameObject.layer == _dialogueLayer)
            {
                ExplanationBox.SetActive(false);
                DialogueBox.SetActive(true);
            }
            else if (_currentObject.transform.gameObject.layer == _explanationLayer)
            {
                ExplanationBox.SetActive(true);
                DialogueBox.SetActive(false);
            }
        }else
        {
            ExplanationBox.SetActive(false);
            DialogueBox.SetActive(false);
        }

        if (_firstPersonController.isInteracting) return;
        Ray cameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(cameraRay.origin, cameraRay.direction * DialogueRange, Color.red);
        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, DialogueRange))
        {
            if (!ShouldInteract(hitInfo))
            {
                CleanInteractions();
                return;
            }
            _currentObject = hitInfo.rigidbody;
            if (_currentObject.transform.gameObject.layer == _dialogueLayer)
            {
                _firstPersonController.Interactable = _currentObject.GetComponent<DialogueActivator>();
            }
            else if (_currentObject.transform.gameObject.layer == _explanationLayer)
            {
                _firstPersonController.ExplanationInt = _currentObject.GetComponent<DialogueExplanation>();
            }
        }
        else
        {
            CleanInteractions();
        }
    }

    private bool ShouldInteract(RaycastHit hitInfo)
    {
        var o = hitInfo.transform.gameObject;
        return o.layer == _dialogueLayer ||
               o.layer == _explanationLayer;
    }
    private void OnDrawGizmos()
    {
        Ray cameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Gizmos.color = Color.red;
        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, DialogueRange))
        {
            Gizmos.DrawSphere(hitInfo.point, 0.1f);
        }
        
    }

    private void CleanInteractions()
    {
        if(_currentObject){
            if (_currentObject.transform.gameObject.layer == _dialogueLayer)
            {
                _currentObject.GetComponent<DialogueActivator>().StopInteracting();
            }
        }
        _currentObject = null;
        _firstPersonController.Interactable = null;
        _firstPersonController.ExplanationInt = null;
    }
}
