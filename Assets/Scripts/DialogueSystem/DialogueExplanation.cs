using UnityEngine;

public class DialogueExplanation : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    
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
        player.DialogueUI.ShowDialogue(dialogueObject);

    }
    
}
