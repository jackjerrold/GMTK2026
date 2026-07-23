using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject lightning;

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

    void Start()
    {
        lightning.SetActive(false);
    }

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
                //ray.collider == null means no collision
                //ray.collider != null means ray.collider has info on the object it collided with

                lightning.SetActive(true); //Make the lightning appear when countdown is reached

                //I tried to avoid big IF statement towers but i js cant here without having RAY not defined or not checking RAY before ROD.ABSORB()
                //Rewrite if you can make it more readable (maybe class variable but idk)
                if (ray.collider == null) { 
                    if (rod.Absorb() == true) {
                        lightning.SetActive(false); //Hides the lightning if the rod absorbed it
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
        } else if (timer >= 1.0f) { //Hide the lightning after x seconds
            lightning.SetActive(false); 
        }

    }

    void FixedUpdate() {
        Debug.Log($"Timer: {timer}");
    }

    private RaycastHit2D Raycast(Vector2 targetPosition) {
        Vector2 startPosition = new Vector2(cloud.position.x, cloud.position.y);
        Vector2 distance = targetPosition - startPosition;
        
        RaycastHit2D ray = Physics2D.Raycast(startPosition, distance.normalized, distance.magnitude, obstacleLayer);

        return ray;
    }
}
