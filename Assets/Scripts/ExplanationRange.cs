using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationRange : MonoBehaviour
{
    [SerializeField] private LayerMask explanationMask;

    [SerializeField] private Camera PlayerCamera;

    [SerializeField] private Transform dialogueTarget;
    [SerializeField] private GameObject talkBox;
    [Space] [SerializeField] private float explanationRange;
    private FirstPersonController firstPersonController;
    public Rigidbody explanationObject;

    private void Start()
    {
        firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if (explanationObject && firstPersonController.isInteracting == false)
        {
            talkBox.SetActive(true);
        }
        else talkBox.SetActive(false);

        if (firstPersonController.isInteracting == false)
        {
            Ray cameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, explanationRange, explanationMask))
            {
                explanationObject = hitInfo.rigidbody;
                firstPersonController.ExplanationInt = explanationObject.GetComponent<DialogueExplanation>();
            }
            else
            {

                    explanationObject = null;
                    firstPersonController.ExplanationInt = null;
                
                
            }
        }
    }
}
