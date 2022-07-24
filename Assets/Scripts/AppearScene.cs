
using UnityEngine;
using UnityEngine.Serialization;


public class AppearScene : MonoBehaviour
{
    [SerializeField] private GameObject scene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scene.SetActive(true);
        }
    }
}
