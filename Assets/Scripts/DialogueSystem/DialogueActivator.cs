using System;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private Boolean shouldTalk = true;
    private Animator animator;
    private static readonly int IsTalking = Animator.StringToHash("IsTalking");
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// Método que viene de la interfaz IInteractable, implementa la interacción entre este dialogo y el jugador
    /// </summary>
    /// <param name="player">GameObject del jugador</param>
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
        
        animator.SetBool(IsTalking, shouldTalk);
        player.DialogueUI.ShowDialogue(dialogueObject);
        
    }

    /// <summary>
    /// Cuando se termine de interactuar, se apaga una bandera del animator
    /// </summary>
    public void StopInteracting()
    {
        animator.SetBool(IsTalking, false);
    }
}
