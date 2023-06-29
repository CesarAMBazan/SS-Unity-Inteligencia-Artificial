using UnityEngine;

// Serialización para crear un objeto de dialogo desde el menú
[CreateAssetMenu(menuName = "Dialogue/DialogueObject")] 
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    [SerializeField] private bool isCorrectQuestion;
    [SerializeField] private int questionLevel;
    public string[] Dialogue => dialogue;

    public Response[] Responses => responses;

    public bool IsCorrectQuestion => isCorrectQuestion;
    public int QuestionLevel => questionLevel;
    public bool HasResponses => Responses != null && Responses.Length > 0;
}
