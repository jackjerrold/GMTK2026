using UnityEngine;
using TMPro;

public class DialogueTest : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    [TextArea(2, 5)]
    public string[] testLines;
    private int currentLineIndex = 0;
    void Start()
    {
        ShowCurrentLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceLine();
        }
    }

    void ShowCurrentLine()
    {
        if (testLines.Length > 0)
        {
            textDisplay.text = testLines[currentLineIndex];
        }
    }

    void AdvanceLine()
    {
        if (currentLineIndex < testLines.Length - 1)
        {
            currentLineIndex++;
        }
        else
        {
            currentLineIndex = 0;
        }
        ShowCurrentLine();
    }
}