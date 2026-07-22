using UnityEngine;
using UnityEngine.InputSystem;

public class RodControls : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private Transform player;
    
    [SerializeField]
    private Vector2 offset = Vector2.zero;

    void Start()
    {
       mainCamera = Camera.main;
    }

    
    void Update()
    {
        if (player != null)
        {
            Vector2 playerPosition = player.position;

            transform.position = playerPosition + offset;
            RotateToMouse();
            

            Debug.Log($"Player Position: {player.position} | Rod Position: {transform.position} | Rod rotation: {transform.rotation}");
        }
        //FollowMousePosition();
    }
    
    private void RotateToMouse()
    {
        if (Mouse.current == null || mainCamera == null) return; 
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = mousePosition - (Vector2)transform.position;
            
            float mouseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, mouseAngle);
        }
    }


}
