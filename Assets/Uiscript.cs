using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiSCRIPT : MonoBehaviour
{
    float Timey = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI timer;
    void Start()
    {

        

        
        
            
        }
    

        
    

    // Update is called once per frame
    void Update()
    {
        if(Timey > 0)
        Timey = Timey - Time.deltaTime;
        timer.text = Timey.ToString("0");
        if(Timey <= 0)
        Timey += Timey + 5;
        
    }
}

