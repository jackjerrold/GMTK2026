using UnityEngine;
using UnityEngine.SceneManagement;

public class NoFadeSceneChangeButton : MonoBehaviour
{
    public string sceneToLoad;
    public bool isQuit;

    void OnMouseUp()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        if (isQuit)
        {
            // only works in built app
            Application.Quit();
        }
    }
}
// dear god please work