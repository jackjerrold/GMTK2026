using UnityEngine;

public class RainComtroller : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offset = 7;

    void Update()
    {
        transform.position = new Vector3(player.position.x + offset, transform.position.y, transform.position.z);
    }
}
