using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;

    void OnMouseUp()
    {
        if (isStart)
        {
            SceneManager.LoadScene(1); 
        }

        if (isQuit)
		// this only does something if its running directly on the PC and not through a website/unity
        {
            Application.Quit();
        }
    }
}
// dear god please work