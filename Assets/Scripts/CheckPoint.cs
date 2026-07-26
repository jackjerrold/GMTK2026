using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform checkpointTransform;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private bool FinalCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (FinalCheckpoint)
            {
                sceneController.NextScene();
            }
            else
            {
                sceneController.CurrentCheckPointPosition = checkpointTransform.position;
                Destroy(gameObject);
            }
        }
    }
}
