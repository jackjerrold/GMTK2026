using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Transform Player;

    public Vector2 CurrentCheckPointPosition;

    private void Start()
    {
        CurrentCheckPointPosition = Player.position;
    }

    public void die()
    {
        //ADD DEATH ANIM
        Player.transform.position = CurrentCheckPointPosition;
    }

    public void NextScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
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
