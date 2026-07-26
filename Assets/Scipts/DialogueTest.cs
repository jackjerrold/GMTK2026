using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueTest : MonoBehaviour
{

    public TextMeshProUGUI textDisplay;
    public GameObject dialoguePanel;
    public AudioSource audioSource;
    public AudioClip solTypeSound;
    public AudioClip mageTypeSound;
    public Image fadeImage;
    public float fadeDuration = 2f;
    public float startDelay = 4f;
    public MonoBehaviour playerMovementScript;
    public Camera mainCamera;
    public Transform solTransform;
    public Transform mageTransform;
    public float defaultCamSize = 5f;
    public float zoomCamSize = 3f;
    public float cameraSpeed = 5f; 
    private Transform currentCameraTarget;    
    private Vector3 defaultCameraPosition;
    [TextArea(2, 5)]
    public string[] testLines;
    public float textSpeed = 0.04f;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool isDialogueActive = false;
    void Start()
    {
        if (mainCamera != null)
        {
            defaultCameraPosition = mainCamera.transform.position;
        }
        if(dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;
            StartCoroutine(FadeFromBlack());
        }
        StartCoroutine(StartDialogueWithDelay());
    }

    IEnumerator StartDialogueWithDelay()
    {
        currentCameraTarget = null;
        yield return new WaitForSeconds(startDelay);
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

        if (mainCamera != null)
        {
            Vector3 targetPos = defaultCameraPosition;
            float targetSize = defaultCamSize;

            if (isDialogueActive && currentCameraTarget != null)
            {
                targetPos = new Vector3(currentCameraTarget.position.x, currentCameraTarget.position.y, -10f);
                targetSize = zoomCamSize;
            }

            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                targetPos,
                Time.deltaTime * cameraSpeed
            );

            mainCamera.orthographicSize = Mathf.Lerp(
                mainCamera.orthographicSize,
                targetSize,
                Time.deltaTime * cameraSpeed
            );
        }
    }

    public void StartDialogue()
    {
        currentLineIndex = 0;
        isDialogueActive = true;
        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        if (playerMovementScript != null) playerMovementScript.enabled = false;
        if (testLines.Length > 0)
        {
            typingCoroutine = StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        textDisplay.text = "";
        string currentLine = testLines[currentLineIndex];
        AudioClip clipToPlay = null;
        if (currentLine.StartsWith("Sol"))
        {
            FocusCameraOn(solTransform);
            clipToPlay = solTypeSound;
        }
        else if (currentLine.StartsWith("Mage"))
        {
            FocusCameraOn(mageTransform);
            clipToPlay = mageTypeSound;
        }
        PlayTypeAudio(clipToPlay);
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
            if (textDisplay != null) textDisplay.text = "";
            if (dialoguePanel != null) dialoguePanel.SetActive(false);
            isDialogueActive = false;
            currentCameraTarget = null;
            StartCoroutine(FadeToBlack());
        }
    }

    IEnumerator FadeFromBlack()
    {
        float timer = 0f;
        Color color = fadeImage.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0f;
        fadeImage.color = color;
    }

    IEnumerator FadeToBlack()
    {
        if (fadeImage != null)
        {
            float timer = 0f;
            Color color = fadeImage.color;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }
            color.a = 1f;
            fadeImage.color = color;
        }
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
    }

    private void FocusCameraOn(Transform target)
    {
        currentCameraTarget = target;
    }

    private void PlayTypeAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
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