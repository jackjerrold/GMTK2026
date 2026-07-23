using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject lightning;

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
        /*
        Moves where the player x is and y
        The placement of the lightning folder thing changes
        The square inside it has a set y offset of 1.5 so the bottom of the lightning is with the bottom of player (temporary)
        */

        if (timer >= countdown) { //Make the lightning appear when countdown is reached
            lightning.SetActive(true);
            timer = 0f;
        } else if (timer >= 1.0f) {
            lightning.SetActive(false); //Hide the lightning after x seconds
        }

        Debug.Log($"Timer: {timer}");
    }
}
