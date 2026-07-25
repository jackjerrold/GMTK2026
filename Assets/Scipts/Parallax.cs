using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxEffect = 0.5f;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 cameraMovement = cameraTransform.position - lastCameraPosition;

        // Move the background based on how much the camera moved
        transform.position += new Vector3(
            cameraMovement.x * parallaxEffect,
            cameraMovement.y * parallaxEffect,
            0f
        );

        lastCameraPosition = cameraTransform.position;
    }
}