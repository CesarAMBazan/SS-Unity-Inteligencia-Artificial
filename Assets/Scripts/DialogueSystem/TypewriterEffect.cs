using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    // Atributo serializable que es la velocidad de escritura
    [SerializeField] private float typewriterSpeed = 80f;
    public bool IsRunning { get; private set; }
    private readonly List<Punctuation> punctuations = new()
    {
        new Punctuation(new HashSet<char>() { '.', '!', '?' }, 0.6f),
        new Punctuation(new HashSet<char>() { ',', ';', ':' }, 0.3f)
    };

    private Coroutine typingCoroutine;
    private TMP_Text textLabel;
    private string textToType;
    
    /// <summary>
    /// Método que se llama para comenzar el efecto de maquina de escribir
    /// </summary>
    /// <param name="textToType">El texto a escribir</param>
    /// <param name="textLabel">El objeto de la interfaz gráfica donde se escribira el texto</param>
    public void Run(string textToType, TMP_Text textLabel)
    {
        this.textToType = textToType;
        this.textLabel = textLabel;
        typingCoroutine = StartCoroutine(TypeText());
    }

    public void Stop()
    {
        if (!IsRunning) return;
        StopCoroutine(typingCoroutine);
        OnTypingCompleted();
    }
    
    /// <summary>
    /// Rútina para escribir el texto como una máquina de escribir
    /// </summary>
    /// <returns></returns>
    private IEnumerator TypeText()
    {
        IsRunning = true;
        textLabel.maxVisibleCharacters = 0;
        textLabel.text = textToType;
        
        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;
            
            t += Time.deltaTime * typewriterSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;
                
                textLabel.maxVisibleCharacters = i+1;

                if (IsPunctuation(textToType[i], out float waitTime) && !isLast &&
                    !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }

        textLabel.maxVisibleCharacters = textToType.Length;
        OnTypingCompleted();
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuactionCategory in punctuations)
        {
            if (punctuactionCategory.Punctuations.Contains(character))
            {
                waitTime = punctuactionCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }

    private void OnTypingCompleted()
    {
        IsRunning = false;
        textLabel.maxVisibleCharacters = textToType.Length;
    }
}
