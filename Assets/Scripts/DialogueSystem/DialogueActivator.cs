using System;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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

    public void Interact(FirstPersonController player)
    {
        animator.SetBool("IsTalking", true);
        player.DialogueUI.ShowDialogue(dialogueObject);
        
    }
}
