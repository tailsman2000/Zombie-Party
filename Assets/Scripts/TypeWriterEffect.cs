using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class TypeWriterEffect : MonoBehaviour
{
    private TMP_Text textBox;

    private int currentTextIndex;

    private WaitForSeconds simpleDelay;
    private Coroutine typewriterCoroutine;
    private WaitForSeconds interpunctuationDelay;

    [Header("Speed of typewriting")]

    [SerializeField] private float characterPerSecond = 20;
    [SerializeField] private float interpunctuationDelaySeconds = 0.8f;

    void Awake()
    {
        textBox = this.GetComponent<TMP_Text>();

        simpleDelay = new WaitForSeconds(1 / characterPerSecond);
        interpunctuationDelay = new WaitForSeconds(interpunctuationDelaySeconds);
    }

    void Start()
    {
        SetText(textBox.text);
    }
    public void SetText(string text)
    {
        textBox.text = text;
        textBox.maxVisibleCharacters = 0;

        currentTextIndex = 0;  

        typewriterCoroutine = StartCoroutine(TypeWriter());
    }

    private IEnumerator TypeWriter()
    {
        TMP_TextInfo textInfo = textBox.textInfo;

        while(currentTextIndex < textInfo.characterCount + 1)
        {
            char character = textInfo.characterInfo[currentTextIndex].character;
            textBox.maxVisibleCharacters++;

            yield return simpleDelay;

            currentTextIndex++;
        }
    }
}
