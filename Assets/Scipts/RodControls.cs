using UnityEngine;
using UnityEngine.InputSystem;

public class RodControls : MonoBehaviour
{
    private Camera mainCamera; //Used to track mouse

    private bool canAbsorb = false;
    private bool isCharged = false;

    public float expellPower = 5;

    //Used to track the time isCharged is true
    private float chargeDuration = 1.5f; 
    private float chargeTimer = 0f;

    [SerializeField]
    private float absorbAngle = 25f; //Angle that lightning can be absorbed (from the horizontal)

    [SerializeField]
    private Transform player;
    private MoveController moveController;


    public Transform rodTip;
    

    [SerializeField]
    private Vector2 positionOffset = Vector2.zero; //Offset from the player (for following the player)

    void Start()
    {
       mainCamera = Camera.main;
       moveController = player.GetComponent<MoveController>();
    }

    
    void Update()
    {
        if (player != null) { //This if part is all for the moving and rotating the rod to the player and mouse
            Vector2 playerPosition = player.position;

            transform.position = playerPosition + positionOffset;
            RotateToMouse();
            
            //        //Debug.Log($"Player Position: {player.position} | Rod Position: {transform.position} | Rod rotation: {transform.rotation}");

            if (transform.eulerAngles.z >= absorbAngle && transform.eulerAngles.z <= 180f-absorbAngle) { //absorbAngle from the horizontal
                canAbsorb = true;
            } else {
                canAbsorb = false;
            }
        }

        if (isCharged) { //isCharged logic with timer
            chargeTimer += Time.deltaTime;

            if (chargeTimer >= chargeDuration) {
                isCharged = false;
                chargeTimer = 0f;
                expell();
            }
        }
    }
    
    private void RotateToMouse() {
        if (Mouse.current == null || mainCamera == null) return; {
            //Checking and getting all the info from the mouse into x, y coords on the game
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
            Vector2 direction = mousePosition - (Vector2)transform.position;
            
            //Using info above to calculate rotation angle (trig)
            float mouseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, mouseAngle);
        }
    }

    public bool Absorb() { //Used to be called in lightning when struck
        if (canAbsorb) {
            isCharged = true;
        }
        return canAbsorb;
    }

    public bool IsCharged() { //Just for the colouring file
        return isCharged;
    }

    private void expell()
    {
        Vector2 Dir  = player.position - rodTip.position;
        moveController.AddExternalForce(Dir*expellPower, true);
    }
}
