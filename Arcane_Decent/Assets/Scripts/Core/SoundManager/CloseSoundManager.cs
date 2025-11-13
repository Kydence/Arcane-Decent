using UnityEngine;

public class CloseSoundManager : MonoBehaviour
{
    public static CloseSoundManager instance {get; private set;}
    AudioSource source;

    void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}
