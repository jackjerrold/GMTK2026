using UnityEngine;
using UnityEngine.InputSystem;


public class RodInGround : MonoBehaviour
{
    public Transform player;
    public GameObject rod;

    void Update()
    {
        if ((transform.position - player.position).magnitude <= 2f && Keyboard.current.eKey.wasPressedThisFrame) {
            Interact();
        }
    }

    public void Interact() {
        rod.SetActive(true);
        Destroy(gameObject);
    }
}
