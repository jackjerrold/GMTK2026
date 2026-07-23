using UnityEngine;

public class Colouring : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer player;

    [SerializeField]
    private SpriteRenderer rod;

    [SerializeField]
    private RodControls rodScript;

    [SerializeField]
    private SpriteRenderer cloud;

    [SerializeField]
    private SpriteRenderer ground;

    void Start()
    {
        player.color = new Color32(255, 255, 255, 255); //White
        rod.color = new Color32(178, 76, 4, 255); //Copper-ish
        cloud.color = new Color32(83, 83, 91, 255); //Grey
        ground.color = new Color32(5, 125, 13, 255); //Green
    }

    
    void Update()
    {
        rod.color = (rodScript.IsCharged()) ? new Color32(14, 191, 192, 255) : new Color32(178, 76, 4, 255);
    }
}
