using UnityEngine;

// Objeto serializable de respuestas
[System.Serializable]
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;

    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
}
