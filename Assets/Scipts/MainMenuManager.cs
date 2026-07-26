using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("OpeningScene");
    }

    public void Settings()
    {
        SceneManager.LoadScene("GameSettings");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu2");
    }
}
