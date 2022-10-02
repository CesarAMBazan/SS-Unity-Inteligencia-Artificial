using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueExplanation : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    
    
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
        player.DialogueUI.ShowDialogue(dialogueObject);

    }
    
}
