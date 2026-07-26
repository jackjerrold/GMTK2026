using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public AudioSource musicSource;

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}