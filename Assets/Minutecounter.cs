using TMPro;
using UnityEngine;

using TMPro;

public class Minutecounter : MonoBehaviour

{

    [SerializeField] TextMeshProUGUI timer;
    float counter = 60;
   
    // Update is called once per frame
    void Update()
    {counter = counter - Time.deltaTime;
     timer.text = counter.ToString("0");
        
    }
}
