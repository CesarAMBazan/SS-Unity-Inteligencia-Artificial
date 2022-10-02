using System;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    private Animator animator;
    private static readonly int IsTalking = Animator.StringToHash("IsTalking");
    
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void Interact(FirstPersonController player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        
        animator.SetBool(IsTalking, true);
        player.DialogueUI.ShowDialogue(dialogueObject);
        
    }

    public void StopInteracting()
    {
        
        animator.SetBool(IsTalking, false);
    }
}
