using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    // variables
    public Image fadeImage;
    public string sceneToLoad;
    public float fadeDuration = 1f;


    // runs at start of scene
    private void Start()
    {
        StartCoroutine(FadeFromBlack());
    }


    // called by buttons that initiate scene change
    public void FadeToScene()
    {
        StartCoroutine(FadeOutAndLoad());
    }


    // fade to black and scene change
    IEnumerator FadeOutAndLoad()
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }


    // fade from black into scene
    IEnumerator FadeFromBlack()
    {
        float t = 0;
        Color c = fadeImage.color;

        c.a = 1;
        fadeImage.color = c;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = 1 - (t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0;
        fadeImage.color = c;
    }
}