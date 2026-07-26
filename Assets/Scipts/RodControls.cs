using UnityEngine;
using UnityEngine.InputSystem;

public class RodControls : MonoBehaviour
{
    private Camera mainCamera; //Used to track mouse

    [SerializeField] private Sprite charged, nonCharged;
    [SerializeField] private SpriteRenderer SpriteRenderer;

    [SerializeField] private CD_BossFight bossFightScr;

    [SerializeField] private bool BossFight;

    public bool canAbsorb = false;
    public bool isCharged = false;

    public bool live = false;

    public float expellPower = 5;

    [SerializeField]
    private float absorbAngle = 25f; //Angle that lightning can be absorbed (from the horizontal)

    [SerializeField]
    private Transform player;

    private MoveController moveController;

    [SerializeField]
    private Lightning lightningManager;

    public Transform rodTip;
    

    [SerializeField]
    private Vector2 positionOffset = Vector2.zero; //Offset from the player (for following the player)

    public ParticleSystem LightningRodTipBurst;
    void Start()
    {
       mainCamera = Camera.main;
       moveController = player.GetComponent<MoveController>();
    }


    void Update()
    {
        if (live)
        {

            SpriteRenderer.enabled = true;

            if (player != null)
            { //This if part is all for the moving and rotating the rod to the player and mouse
                Vector2 playerPosition = player.position;

                transform.position = playerPosition + positionOffset;
                RotateToMouse();

                //        //Debug.Log($"Player Position: {player.position} | Rod Position: {transform.position} | Rod rotation: {transform.rotation}");

                if (transform.eulerAngles.z >= absorbAngle && transform.eulerAngles.z <= 180f - absorbAngle && !isCharged)
                { //absorbAngle from the horizontal
                    canAbsorb = true;
                }
                else
                {
                    canAbsorb = false;
                }
            }

            if (isCharged)
            { //isCharged logic with timer

                SpriteRenderer.sprite = charged;

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    expell();
                }
            }
            else
            {
                SpriteRenderer.sprite = nonCharged;
            }
        }
        else
        {
            SpriteRenderer.enabled = false;
        }

    }
    
    private void RotateToMouse() {
        if (Mouse.current == null || mainCamera == null) return; {
            //Checking and getting all the info from the mouse intss x, y coords on the games
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
            Vector2 direction = mousePosition - (Vector2)transform.position;
            
            //Using info above to calculate rotation angle (trig)
            float mouseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, mouseAngle);
        }
    }

    public bool Absorb() { //Used to be called in lightning when struck
        if (canAbsorb && live) {
            isCharged = true;
            moveController.AbsorptionBoost();
            LightningRodTipBurst.Emit(30);
        }
        return (canAbsorb && live);
    }

    public bool IsCharged() { //Just for the colouring file
        return isCharged;
    }

    private void expell()
    {
        isCharged = false;

        LightningRodTipBurst.Emit(30);
        Vector2 Dir = rodTip.position - player.position;
        moveController.AddExternalForce(-Dir * expellPower, true);

        if (BossFight)
        {
            bossFightScr.ExpellLightning(rodTip, Dir);
        }
        else
        {
            lightningManager.ExpellLightning(rodTip, Dir);
        }
    }

    public bool BossFightAttack()//returns weather or not it can recive an attack
    {
        if (transform.eulerAngles.z >= 180 - absorbAngle && transform.eulerAngles.z <= 180f + absorbAngle && !isCharged)
        {
            isCharged = true;
            moveController.AbsorptionBoost();
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
