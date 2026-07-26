using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class DialogueTest : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    public GameObject dialoguePanel;
    public AudioSource audioSource;
    public AudioClip typeSound;
    [TextArea(2, 5)]
    public string[] testLines;
    public float textSpeed = 0.04f;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool isDialogueActive = false;
    void Start()
    {
        StartDialogue();
    }

    void Update()
    {
        if (isDialogueActive && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (textDisplay.text != testLines[currentLineIndex])
            {
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                textDisplay.text = testLines[currentLineIndex];
                StopTypeAudio();
            }
            else
            {
                AdvanceLine();
            }
        }
    }

    public void StartDialogue()
    {
        currentLineIndex = 0;
        isDialogueActive = true;
        dialoguePanel.SetActive(true);

        if (testLines.Length > 0)
        {
            typingCoroutine = StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        textDisplay.text = "";
        PlayTypeAudio();
        foreach (char letter in testLines[currentLineIndex].ToCharArray())
        {
            textDisplay.text += letter; 
            yield return new WaitForSeconds(textSpeed); 
        }
        StopTypeAudio();
    }

    void AdvanceLine()
    {
        if (currentLineIndex < testLines.Length - 1)
        {
            currentLineIndex++;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            StopTypeAudio();
            textDisplay.text = "";
            dialoguePanel.SetActive(false);
            isDialogueActive = false;
        }
    }


    private void PlayTypeAudio()
    {
        if (audioSource != null && typeSound != null)
        {
            audioSource.clip = typeSound;
            audioSource.loop = true; 
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void StopTypeAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}