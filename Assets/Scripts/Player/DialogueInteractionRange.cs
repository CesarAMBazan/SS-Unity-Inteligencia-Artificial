using UnityEngine;
using UnityEngine.Serialization;

public class DialogueInteractionRange : MonoBehaviour
{
    // Atributos de la clase, serializables para poder ser configurables.
    [SerializeField] private Camera playerCamera;
    
    [Space] [SerializeField] private GameObject dialogueBox;

    [SerializeField] private GameObject explanationBox;
    
    [Space] [SerializeField] private float dialogueRange;
    
    // Atributos privados
    private FirstPersonController _firstPersonController;
    private Rigidbody _currentObject;
    private LayerMask _dialogueLayer;
    private LayerMask _explanationLayer;

    /// <summary>
    /// El método Start corre automaticamente al crear el GameObject al que este componente esta enlazado.
    /// </summary>
    private void Start()
    {
        // Se obtiene de la escena el GameObject de nombre "Player", a este GameObject se le toma su componente
        // FirstPersonController
        _firstPersonController = GameObject.Find("Player").GetComponent<FirstPersonController>();
        
        // Se define la capa de dialogo con NPC o de explicación con Exp
        _dialogueLayer = LayerMask.NameToLayer("NPC");
        _explanationLayer = LayerMask.NameToLayer("Exp");
    }

    /// <summary>
    /// Método Update que se llama cada Frame
    /// </summary>
    private void Update()
    {
        // Sí existe un objeto con el cual interactuar y el jugador no esta interactuando con nada
        if (_currentObject && _firstPersonController.isInteracting == false)
        {
            // Sí el objeto actual se trata de un objeto que permite el dialogo.
            if (_currentObject.transform.gameObject.layer == _dialogueLayer)
            {
                // Se muestra el texto de dialogo
                explanationBox.SetActive(false);
                dialogueBox.SetActive(true);
            }
            // Sí se trata de un objeto de explicación
            else if (_currentObject.transform.gameObject.layer == _explanationLayer)
            {
                // Se muestra el texto de explicación
                explanationBox.SetActive(true);
                dialogueBox.SetActive(false);
            }
        }
        else
        {
            // Sí no se trata de ninguno, se ocultan los dos textos
            explanationBox.SetActive(false);
            dialogueBox.SetActive(false);
        }

        // Sí el jugador ya esta interactuando con un objeto, se detiene el método
        if (_firstPersonController.isInteracting) return;
        
        // Se crea un raycast que nace desde la cámara del jugador
        var cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        
        // Se dibuja el raycast para debuggear.
        Debug.DrawRay(cameraRay.origin, cameraRay.direction * dialogueRange, Color.red);
        
        // Sí el raycast golpea a un objeto de dialogo
        if (Physics.Raycast(cameraRay, out var hitInfo, dialogueRange))
        {
            // Sí no se debe interactuar
            if (!ShouldInteract(hitInfo))
            {
                // Se limpia la interacción y se termina el método
                CleanInteractions();
                return;
            }

            // Sí si se puede interactuar con este objeto, el objeto actual toma el valor del objeto que
            // choco con el raycast
            _currentObject = hitInfo.rigidbody;
            
            // Sí se trata de un objeto de dialogo, el objeto con el que interactua el jugador viene de
            // DialogueActivator
            if (_currentObject.transform.gameObject.layer == _dialogueLayer)
                _firstPersonController.Interactable = _currentObject
                    .GetComponent<DialogueActivator>();
            // Sí se trata de un objeto de explicación, el objeto con el que interactua el jugador viene de
            // DialogueExplanation
            else if (_currentObject.transform.gameObject.layer == _explanationLayer)
                _firstPersonController.ExplanationInt = _currentObject
                    .GetComponent<DialogueExplanation>();
        }
        else
        {
            // Se limpian las interacciones con el sistema de dialogos
            CleanInteractions();
        }
    }

    /// <summary>
    /// Método que indica sí el objeto que choco con el raycast pertenece a las capas de dialogo o de explicación
    /// </summary>
    /// <param name="hitInfo">Objeto que choco con el raycast</param>
    /// <returns>True o False dependiendo si el objeto pertenece a alguna de estas dos capas</returns>
    private bool ShouldInteract(RaycastHit hitInfo)
    {
        var o = hitInfo.transform.gameObject;
        return o.layer == _dialogueLayer ||
               o.layer == _explanationLayer;
    }

    /// <summary>
    /// Método debug para dibujar un circulo rojo oscuro cuando el raycast choca con un objeto
    /// </summary>
    private void OnDrawGizmos()
    {
        var cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Gizmos.color = Color.red;
        if (Physics.Raycast(cameraRay, out var hitInfo, dialogueRange)) Gizmos.DrawSphere(hitInfo.point, 0.1f);
    }

    /// <summary>
    /// Método para limpiar interacciones, como los objetos del raycast se actualizan cada frame, es importante
    /// limpiar el objeto interactuable en caso de que el jugador no este interactuando con ningun objeto del tipo
    /// dialogo o explicación
    /// </summary>
    private void CleanInteractions()
    {
        if (_currentObject)
            if (_currentObject.transform.gameObject.layer == _dialogueLayer)
                _currentObject.GetComponent<DialogueActivator>().StopInteracting();

        _currentObject = null;
        _firstPersonController.Interactable = null;
        _firstPersonController.ExplanationInt = null;
    }
}