using UnityEngine;

public class PhysicsPickup : MonoBehaviour
{
    // Atributos de la clase, serializables para poder ser configurables.
    [SerializeField] private LayerMask pickupMask;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private Transform pickupTarget;

    [SerializeField] private GameObject textPickup;

    [Space] [SerializeField] private float pickupRange;

    // Atributos privados
    private FirstPersonController _firstPersonController;
    private Rigidbody _currentObject;

    /// <summary>
    /// El método Start corre automaticamente al crear el GameObject al que este componente esta enlazado.
    /// </summary>
    private void Start()
    {
        // Se obtiene el componente FirstPersonController de la escena buscando
        // al GameObject con nombre 'Player'
        _firstPersonController = GameObject.Find("Player")
            .GetComponent<FirstPersonController>();
    }

    /// <summary>
    /// Método Update que se llama cada Frame
    /// </summary>
    private void Update()
    {
        // Se crea un pequeño rayo que nace desde la ubicación de la cámara del jugador
        var cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        // Sí este rayo 'golpea' a un objeto que se encuentra en la capa pickupMask
        // y el jugador no esta interactuando.
        if (Physics.Raycast(cameraRay, out var hit, pickupRange, pickupMask) &&
            _firstPersonController.isInteracting == false && _firstPersonController.Interactable == null)
            // Se activa el texto que indica que el jugador puede agarrar un objeto
            textPickup.SetActive(true);
        else textPickup.SetActive(false); // De otra manera este texto en la interfaz gráfica se oculta

        // Si el jugador presiona la tecla 'E' y el jugador no esta interactuando
        if (Input.GetKeyDown(KeyCode.E) && _firstPersonController.Interactable == null)
        {
            // Sí el jugador ya esta agarrando un objeto actualmente
            if (_currentObject)
            {
                // EL objeto vuelve a ser afectado por la gravedad
                _currentObject.useGravity = true;
                // El objeto deja de ignorar las colisiones con el jugador
                Physics.IgnoreCollision(GameObject.Find("Player")
                        .GetComponent<Collider>(),
                    _currentObject
                        .GetComponent<Collider>(), false);
                // Se cambian los parametros para indicar que el jugador ya no está interactuando con ningún objeto
                _currentObject = null;
                _firstPersonController.isInteracting = false;
                return;
            }

            // En caso de que el jugador no este agarrando un objeto actualmente y el rayo que nace desde la camara
            // choca con un objeto en la capa pickupMask
            if (Physics.Raycast(cameraRay, out var hitInfo, pickupRange, pickupMask))
            {
                // Se indica que el jugador esta agarrando un objeto
                _currentObject = hitInfo.rigidbody;
                // El objeto deja de tener gravedad
                _currentObject.useGravity = false;
                // El objeto ignorará la colisión con el jugador
                Physics.IgnoreCollision(GameObject.Find("Player")
                        .GetComponent<Collider>(),
                    _currentObject
                        .GetComponent<Collider>(), true);
                // Se levanta la bandera para indicar que el jugador está interactuando con un objeto
                _firstPersonController.isInteracting = true;
            }
        }
    }

    /// <summary>
    /// El método fixed update se ejecuta en intervalos regulares de tiempo.
    /// </summary>
    private void FixedUpdate()
    {
        // Sí existe un objeto
        if (_currentObject)
        {
            // Se realizan los cambios para que el objeto se mueva a partir de
            // un GameObject auxiliar llamado pickupTarget
            var directionToPoint = pickupTarget.position - _currentObject.position;
            var distanceToPoint = directionToPoint.magnitude;

            _currentObject.velocity = directionToPoint * (12f * distanceToPoint);
        }
        // Si no existe un objeto y el jugador no esta interactuando con nada
        else if (!_currentObject && _firstPersonController.isInteracting && _firstPersonController.GetInteractions)
        {
            // Se declara la bandera de interacción como false
            _firstPersonController.isInteracting = false;
        }
    }
}