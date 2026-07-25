using UnityEngine;

public class SFXToggle : MonoBehaviour
{
    public AudioSource[] sfxSources;

    public void ToggleSFX(bool isOn)
    {
        foreach (AudioSource source in sfxSources)
        {
            source.mute = !isOn;
        }
    }
}