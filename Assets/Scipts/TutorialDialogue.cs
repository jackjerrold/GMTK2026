using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialDialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject dialoguePanel;
    public AudioSource audioSource;
    public AudioClip solTypeSound;
    public MonoBehaviour playerMovementScript;
    public Camera mainCamera;
    public Transform solTransform;
    public float defaultCamSize = 5f;
    public float zoomCamSize = 3f;
    public float cameraSpeed = 5f;
    [TextArea(2, 5)]
    public string[] dialogueLines;
    public float textSpeed = 0.04f;
    public TutorialUIScript tutorialPopupScript; 
    private Transform currentCameraTarget;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;
    private bool isDialogueActive = false;
    private bool hasTriggered = false;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if (mainCamera != null)
        {
            defaultCamSize = mainCamera.orthographicSize;
        }
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartDialogue();
        }
    }

    void Update()
    {
        if (isDialogueActive && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (textDisplay.text != dialogueLines[currentLineIndex])
            {
                if (typingCoroutine != null) StopCoroutine(typingCoroutine);
                textDisplay.text = dialogueLines[currentLineIndex];
                StopTypeAudio();
            }
            else
            {
                AdvanceLine();
            }
        }

        if (isDialogueActive && mainCamera != null && currentCameraTarget != null)
        {
            Vector3 targetPos = new Vector3(currentCameraTarget.position.x, currentCameraTarget.position.y, -10f);

            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                targetPos,
                Time.deltaTime * cameraSpeed
            );

            mainCamera.orthographicSize = Mathf.Lerp(
                mainCamera.orthographicSize,
                zoomCamSize,
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
        FocusCameraOn(solTransform != null ? solTransform : transform);

        if (dialogueLines.Length > 0)
        {
            typingCoroutine = StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        textDisplay.text = "";
        string currentLine = dialogueLines[currentLineIndex];

        PlayTypeAudio(solTypeSound);

        foreach (char letter in currentLine.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
        StopTypeAudio();
    }

    void AdvanceLine()
    {
        if (currentLineIndex < dialogueLines.Length - 1)
        {
            currentLineIndex++;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        StartCoroutine(ResetCameraAndOpenTutorial());
    }

    IEnumerator ResetCameraAndOpenTutorial()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPos = mainCamera.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            Vector3 targetPos = new Vector3(solTransform.position.x, solTransform.position.y, -10f);
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);
            mainCamera.orthographicSize = Mathf.Lerp(zoomCamSize, defaultCamSize, t);
            yield return null;
        }
        mainCamera.orthographicSize = defaultCamSize;
        isDialogueActive = false;
        if (tutorialPopupScript != null)
        {
            tutorialPopupScript.OpenTutorial();
        }
        else
        {
            if (playerMovementScript != null) playerMovementScript.enabled = true;
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