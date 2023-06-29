using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;
    private List<GameObject> tempResponseButtons = new List<GameObject>();
    private ResponseEvent[] responseEvents;
    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }
    /// <summary>
    /// Método que muestra las respuestas del dialogo actual
    /// </summary>
    /// <param name="responses">Array con respuestas</param>
    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;

        for (int i = 0; i< responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;
            
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text=response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));
            
            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);


    }

    /// <summary>
    /// Método que identifica la respuesta clickeada por el usuario
    /// </summary>
    /// <param name="response">Respuesta clickeada</param>
    /// <param name="responseIndex">Indice de la respuesta</param>
    private void OnPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);
        foreach (var button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();

        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        responseEvents = null;

        if (response.DialogueObject)
        {
            dialogueUI.ShowDialogue(response.DialogueObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }
        
    }
}
