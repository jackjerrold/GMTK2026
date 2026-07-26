using UnityEngine;
using UnityEngine.Video;

public class TutorialUIScript : MonoBehaviour
{
    public GameObject tutorialPopupPanel;
    public GameObject page1;
    public GameObject page2;
    public MonoBehaviour playerMovementScript;
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;

    public void OpenTutorial()
    {
        tutorialPopupPanel.SetActive(true);
        page1.SetActive(true);
        page2.SetActive(false);
        if (videoPlayer1 != null)
        {
            videoPlayer1.Play();
        }
        Time.timeScale = 0f; 
    }

    public void ShowPageTwo()
    {
        page1.SetActive(false);
        page2.SetActive(true);
        if (videoPlayer2 != null)
        {
            videoPlayer2.Play();
        }
    }

    public void CloseTutorial()
    {
        tutorialPopupPanel.SetActive(false);
        Time.timeScale = 1f;
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
    }
}