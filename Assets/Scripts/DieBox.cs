using UnityEngine;

public class DieBox : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sceneController.die();
        }
    }
}
