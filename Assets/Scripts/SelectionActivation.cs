using System;
using System.Collections;
using UnityEngine;


public class SelectionActivation : MonoBehaviour, IInteractable
{
    [SerializeField] private int OwnValue;
    private GameObject LevelListener;

    private void Start()
    {
        LevelListener = GameObject.Find("Level4Listener");
    }

    public void Interact(FirstPersonController player)
    {
        StartCoroutine(InteractCoroutine());
    }

    IEnumerator InteractCoroutine()
    {
        yield return new WaitForSeconds(1);
        LevelListener.GetComponent<Level4Listener>().MakeAChoice(OwnValue);
    }
    
}