using UnityEngine;

public class FarSoundZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (FarSoundManager.instance != null)
            {
                FarSoundManager.instance.SetPlayerInZone(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (FarSoundManager.instance != null)
            {
                FarSoundManager.instance.SetPlayerInZone(false);
            }
        }
    }
}
