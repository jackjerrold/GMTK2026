using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueTest : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    public GameObject dialoguePanel;
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
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (textDisplay.text != testLines[currentLineIndex])
            {
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                textDisplay.text = testLines[currentLineIndex];
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

        foreach (char letter in testLines[currentLineIndex].ToCharArray())
        {
            textDisplay.text += letter; 
            yield return new WaitForSeconds(textSpeed); 
        }
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
            textDisplay.text = "";
            dialoguePanel.SetActive(false);
            isDialogueActive = false;
        }
    }
}