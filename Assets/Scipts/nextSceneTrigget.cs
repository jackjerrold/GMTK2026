using UnityEngine;

public class nextSceneTrigget : MonoBehaviour
{
    private SceneController sceneController;
    private void Start()
    {
        sceneController = FindAnyObjectByType<SceneController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(sceneController != null)
        {
            sceneController.NextScene();
        }
        else
        {
            Debug.LogError("SceneController not found in the scene.");
        }
    }
}
