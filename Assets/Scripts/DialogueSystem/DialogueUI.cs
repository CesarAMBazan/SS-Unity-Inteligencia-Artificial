using System;
using System.Collections;
using UnityEngine;
using TMPro;
public class DialogueUI : MonoBehaviour
{
    
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;


    public bool IsOpen { get; private set; }
    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;
    private FirstPersonController firstPersonController;
    private void Start()
    {
        firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
        responseHandler = GetComponent<ResponseHandler>();
        typewriterEffect = GetComponent<TypewriterEffect>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        lockPlayer();
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
        
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }
    public void CloseDialogueBox()
    {
        IsOpen = false;
        unlockPlayer();
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    private void lockPlayer()
    {
        firstPersonController.isInteracting = true;
        firstPersonController.lockCursor = false;
        firstPersonController.cameraCanMove = false;
        firstPersonController.playerCanMove = false;
        firstPersonController.enableJump = false;
        firstPersonController.enableCrouch = false;
        firstPersonController.enableSprint = false;
    }

    private void unlockPlayer()
    {
        firstPersonController.isInteracting = false;
        firstPersonController.lockCursor = true;
        firstPersonController.cameraCanMove = true;
        firstPersonController.playerCanMove = true;
        firstPersonController.enableJump = true;
        firstPersonController.enableCrouch = true;
        firstPersonController.enableSprint = true;
    }
}
