using UnityEngine;

public class SpeedRunTimer : MonoBehaviour
{
    public float currentTime;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }
}
