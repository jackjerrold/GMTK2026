using UnityEngine;
using UnityEngine.InputSystem;


public class RodInGround : MonoBehaviour
{
    public Transform player;
    public bool isTaken = false;

    public GameObject rodInGround;
    public GameObject rod;

    void Start()
    {
        
    }

    void Update()
    {
        if ((transform.position - player.position).magnitude <= 2f && Keyboard.current.eKey.wasPressedThisFrame) {
            Interact();
        }
        rodInGround.SetActive(!isTaken);
        rod.SetActive(isTaken);
    }

    public void Interact() {
        isTaken = true;
    }
}
