using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level4Listener : MonoBehaviour
{
    private int WordObjective { get; set; }
    public GameObject television;
    public List<Material> chineseWords;
    public int CurrentScore;
    [SerializeField] private int CompleteScore = 4;
    public GameObject ExitDoor;

    public bool testRandom;

    private void Update()
    {
        if (testRandom)
        {
            MakeAChoice(WordObjective);
            testRandom = !testRandom;
        }

    }

    private void Start()
    {
        SetRandomWord();
    }

    public void MakeAChoice(int answer)
    {
        // Jugador escoge una palabra
        // ¿Es correcta?
        if (answer == WordObjective)
        {
            // La respuesta es correcta
            CurrentScore++;
            // ¿Lleva el numero completo de correctas?
            if (CurrentScore == CompleteScore)
            {
                GameOver();
            }
            else
            {
                SetRandomWord();
            }
        }
    }
    public void SetRandomWord()
    {
        int selected = Random.Range(0, chineseWords.Count);
        var televisionCopy = television.GetComponent<MeshRenderer>().materials;
        televisionCopy[1] = chineseWords[selected];
        String materialIndexName = chineseWords[selected].name;
        var resultString = Regex.Match(materialIndexName, @"\d+").Value;
        WordObjective = Int32.Parse(resultString);
        Debug.Log(WordObjective);
        television.GetComponent<MeshRenderer>().materials = televisionCopy;
        if (chineseWords.Count > 1) 
        {
            chineseWords.RemoveAt(selected);
        }
    }

    private void GameOver()
    {
        ExitDoor.GetComponent<Door>().Open(transform.position);
        Debug.Log("GRACIAS POR JUGAR");
    }

}
