using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private DrawLightning drawLightning;

    private GameObject lightning;
    private float lightningDuration = 1.0f;

    [SerializeField]
    private Transform cloud;

    [SerializeField]
    private RodControls rod;


    [SerializeField]
    private LayerMask obstacleLayer; //Layer at which the obstacles will be (in Sprite Renderer)
    //The player and rod is at 0 rn and the lightning is at -1 so its behind the player but thats temporary.
    //I think the cloud and lightning will be set at some foreground value (especially cloud) so js lmk or tell the level design ppl or whatever idk


    [SerializeField]
    private float countdown = 5f; //Holy Peak

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        transform.position = new Vector2(player.position.x, player.position.y);
        //cloud.position = new Vector2(player.position.x, player.position.y);

        /*
        Moves where the player x is and y
        The placement of the lightning folder thing changes
        The square inside it has a set y offset of 1.5 so the bottom of the lightning is with the bottom of player (temporary)
        */


        if (timer >= countdown) { 
                RaycastHit2D ray = Raycast(player.position);

                CreateLightning(ray);

                if (ray.collider == null) { 
                    if (rod.Absorb() == true) {
                        Destroy(lightning); //Hides the lightning if the rod absorbed it
                    }
                } else {
                    /*
                    i got no code to write here cause idk how u want it to break
                    (either js Destroy() or some Break() method for animations)
                    but you can access the object through the RaycastHit2D ray variable above

                    Destroy(ray.collider.gameObject);
                    or
                    ray.collider.gameObject.Break();
                    .. or however it should look like
                    */
                }
                timer = 0f;
        }
    }

    void FixedUpdate() {
        Debug.Log($"Timer: {timer}");
    }

    private RaycastHit2D Raycast(Vector2 startPosition) {
        Vector2 targetPosition = new Vector2(cloud.position.x, cloud.position.y);
        Vector2 distance = targetPosition - startPosition;
        
        RaycastHit2D ray = Physics2D.Raycast(startPosition, distance.normalized, distance.magnitude, obstacleLayer);

        return ray;
    }

    private void CreateLightning(RaycastHit2D ray) {
        lightning = Instantiate(prefab, prefab.transform.position, Quaternion.identity);
        drawLightning.startPoint = cloud.transform;
        drawLightning.endPoint = (ray.collider != null) ? ray.transform : player.transform;
        Destroy(lightning, lightningDuration);
    }
}
