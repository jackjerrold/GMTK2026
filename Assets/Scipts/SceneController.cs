using UnityEngine;

public class SceneController : MonoBehaviour
{
    private int currentSceneIndex = 0;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NextScene()
    {
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load.");
        }
    }
}
