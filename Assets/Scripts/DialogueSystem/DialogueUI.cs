using System;
using System.Collections;
using UnityEngine;
using TMPro;
public class DialogueUI : MonoBehaviour
{
    /* Atributos serializables para ser configurados en el editor */
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;


    
    public bool IsOpen { get; private set; }
    /* Atributos Privados */
    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;
    private FirstPersonController firstPersonController;
    
    /// <summary>
    /// Función que se llama al primer frame del juego.
    /// </summary>
    private void Start()
    {
        // Se obtiene una referencia al gameobject de jugador
        firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
        // Se obtiene una referencia a los componentes del sistema de dialogo
        responseHandler = GetComponent<ResponseHandler>();
        typewriterEffect = GetComponent<TypewriterEffect>();
        // Se cierra la ventana de dialogo
        CloseDialogueBox();
    }

    /// <summary>
    /// Método que muestra un dialogo
    /// </summary>
    /// <param name="dialogueObject">Objeto de dialogo</param>
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        // Se activa la caja de dialogo
        dialogueBox.SetActive(true);
        // Se bloquea al jugador
        lockPlayer();
        // Se indica que el jugador esta interactuando
        firstPersonController.isInteracting = true;
        
        // Si el objeto de dialogo se trata de una respuesta correcta se llama al método para subir el contador
        if (dialogueObject.IsCorrectQuestion)
        {
            firstPersonController.RespuestaCorrecta(dialogueObject.QuestionLevel);
        }
        // Se crea una corutina que muestra el dialogo en pantalla
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    /// <summary>
    /// Método que añade eventos de respuesta al sistema de dialogos
    /// </summary>
    /// <param name="responseEvents">Un array de eventos de respuesta</param>
    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }
    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// Método que escribe el dialogo en pantalla con un efecto de máquina de escribir
    /// </summary>
    /// <param name="dialogueObject">El objeto de dialogo a escribir</param>
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        // Ciclo flor que escribe el dialogo letra por letra aparentando un efecto de maquina de escribir
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            // Sí el jugador presiona espacio se completa el dialogo
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        // Sí este dialogo cuenta con respuestas configuradas se muestran
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        // Sino se cierra la caja de dialogo
        else
        {
            CloseDialogueBox();
        }
        
    }

    /// <summary>
    /// Método que imprime un string como una maquina de escribir
    /// </summary>
    /// <param name="dialogue">Cádena a imprimir</param>
    /// <returns></returns>
    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;
            // Sí se presiona espacio, se termina el efecto y el dialogo termina
            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }
    
    /// <summary>
    /// Método para cerrar la caja de dialogo
    /// </summary>
    public void CloseDialogueBox()
    {
        IsOpen = false;
        unlockPlayer();
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    /// <summary>
    /// Método para bloquear al jugador de hacer movimientos
    /// </summary>
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

    /// <summary>
    /// Método para desbloquear al jugador para hacer movimientos
    /// </summary>
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
