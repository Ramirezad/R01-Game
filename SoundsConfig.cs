using UnityEngine;

public class PlaySoundOnAnimation : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound1()
    {
        PlaySound(sound1);
    }

    public void PlaySound2()
    {
        PlaySound(sound2);
    }

    public void PlaySound3()
    {
        PlaySound(sound3);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
