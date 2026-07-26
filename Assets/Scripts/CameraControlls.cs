using UnityEngine;

public class CameraControlls : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.position;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, playerPosition.x, 0.2f), 0, -10);
        }
    }
}
