using System;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    // [SerializeField] private LayerMask dialogueMask;

    // [SerializeField] private Camera PlayerCamera;

    // [SerializeField] private Transform dialogueTarget;

    // [Space] [SerializeField] private float dialogueRange;
    // private FirstPersonController firstPersonController;
    private Animator animator;
    private static readonly int IsTalking = Animator.StringToHash("IsTalking");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*private void Update()
    {
        Ray cameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, dialogueRange, dialogueMask))
        {
            firstPersonController.Interactable = this;
        }
    }*/
/*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out FirstPersonController player))
        {
            player.Interactable = this;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out FirstPersonController player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
                animator.SetBool("IsTalking", false);
            }
        }
    }
*/
    public void Interact(FirstPersonController player)
    {
        animator.SetBool(IsTalking, true);
        player.DialogueUI.ShowDialogue(dialogueObject);
        
    }

    public void stopInteracting()
    {
        animator.SetBool(IsTalking, false);
    }
}
