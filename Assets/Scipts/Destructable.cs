using UnityEngine;

public class Destructable : MonoBehaviour
{
    public void Destruct()
    {
        //Will probably add anims later
        Destroy(gameObject);
    }
}
