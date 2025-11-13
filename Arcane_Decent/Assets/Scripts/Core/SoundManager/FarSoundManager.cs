using UnityEngine;

public class FarSoundManager : MonoBehaviour
{
    public static FarSoundManager instance {get; private set;}
    AudioSource source;
    bool playerInZone = false;

    void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        if (!playerInZone || sound == null)
        {
            return;
        }
        source.PlayOneShot(sound);
    }

    public void SetPlayerInZone(bool inZone)
    {
        playerInZone = inZone;
    }
}
