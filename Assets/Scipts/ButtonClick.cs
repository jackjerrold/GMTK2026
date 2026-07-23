using UnityEngine;
using UnityEngine.SceneManagement; // Recommended for scene loading

public class ButtonClick : MonoBehaviour
{
    // Variables must be INSIDE the class
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