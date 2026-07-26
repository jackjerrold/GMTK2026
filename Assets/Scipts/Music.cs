using UnityEngine;

public class Music : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip[] tracks;

    [Header("Settings")]
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool loopPlaylist = true;
    [SerializeField] private bool shuffle = false;

    private AudioSource audioSource;
    private int currentTrack = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false; // We handle looping ourselves
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (playOnStart && tracks.Length > 0)
        {
            PlayTrack(currentTrack);
        }
    }

    private void Update()
    {
        if (tracks.Length == 0)
            return;

        if (!audioSource.isPlaying)
        {
            NextTrack();
        }
    }

    private void PlayTrack(int index)
    {
        if (index < 0 || index >= tracks.Length)
            return;

        audioSource.clip = tracks[index];
        audioSource.Play();
    }

    public void NextTrack()
    {
        if (tracks.Length == 0)
            return;

        if (shuffle)
        {
            currentTrack = Random.Range(0, tracks.Length);
        }
        else
        {
            currentTrack++;

            if (currentTrack >= tracks.Length)
            {
                if (loopPlaylist)
                    currentTrack = 0;
                else
                    return;
            }
        }

        PlayTrack(currentTrack);
    }

    public void PreviousTrack()
    {
        if (tracks.Length == 0)
            return;

        currentTrack--;

        if (currentTrack < 0)
            currentTrack = tracks.Length - 1;

        PlayTrack(currentTrack);
    }

    public void PlayTrackByIndex(int index)
    {
        if (index < 0 || index >= tracks.Length)
            return;

        currentTrack = index;
        PlayTrack(currentTrack);
    }
}
