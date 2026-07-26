using UnityEngine;
using UnityEngine.Rendering;

public class SFX : MonoBehaviour
{
    public float volume = 1.0f;

    public AudioSource RainSounds, LightningSound;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateVolume(float volume)
    {
        RainSounds.volume = volume;
        LightningSound.volume = volume;
    }
}
