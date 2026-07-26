using UnityEngine;

public class Boss_Fight_Platorm_Gemerator : MonoBehaviour
{
    public GameObject Repeat;
    public float offset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(Repeat,transform.position + (Vector3.right * offset),Quaternion.identity);
        Destroy(gameObject);
    }
}
