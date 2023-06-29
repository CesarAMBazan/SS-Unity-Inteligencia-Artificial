using UnityEngine;


public class SelectionRange : MonoBehaviour
{
    // Atributos de la clase, serializables para poder ser configurables.
    [SerializeField] private LayerMask selectionMask;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private GameObject selectionBox;

    [SerializeField] private float selectionRange;

    // Atributos privados
    private FirstPersonController _firstPersonController;
    private Rigidbody _currentObject;

    /// <summary>
    /// El método Start corre automaticamente al crear el GameObject al que este componente esta enlazado.
    /// </summary>
    private void Start()
    {
        // Se obtiene de la escena el GameObject de nombre "Player", a este GameObject se le toma su componente
        // FirstPersonController
        _firstPersonController = GameObject.Find("Player")
            .GetComponent<FirstPersonController>();
    }

    /// <summary>
    /// Método Update que se llama cada Frame
    /// </summary>
    private void Update()
    {
        // Sí existe un objeto con el cual interactuar y el jugador no esta interactuando con nada
        if (_currentObject && _firstPersonController.isInteracting == false)
        {
            // Se muestra el texto de selección
            selectionBox.SetActive(true);
        }
        else selectionBox.SetActive(false);

        // Sí el jugador no esta interactuando
        if (_firstPersonController.isInteracting == false)
        {
            // Se declara un raycast que nace desde la camara del jugador
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            // Sí el raycast colisiona con un objeto dentro de la capa de selección
            if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, selectionRange, selectionMask))
            {
                // Se asigna ese objeto a nuestro objeto a interactuar
                _currentObject = hitInfo.rigidbody;
                // Se envia al firstPersonController el SelectionActivation del objeto a interactuar
                _firstPersonController.SelectionInt = _currentObject
                    .GetComponent<SelectionActivation>();
            }
            else
            {
                // Se limpia la interacción.
                _currentObject = null;
                _firstPersonController.SelectionInt = null;
            }
        }
    }
}