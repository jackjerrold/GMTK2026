using UnityEngine;

public class Parallax : MonoBehaviour
{

    private Vector2 startPos;

    public float offsetX = 0f;
    public float offsetY = 0f;
    public Camera cam;
    public float parallaxStrength = 1.0f;
    void Start()
    {
        startPos = transform.position;
        cam = Camera.main;
    }


    void Update()
    {
        //float distance = cam.transform.position * parallaxStrength;
        transform.position = ((Vector2)cam.transform.position * parallaxStrength) + new Vector2 (offsetX, offsetY);
    }
}
