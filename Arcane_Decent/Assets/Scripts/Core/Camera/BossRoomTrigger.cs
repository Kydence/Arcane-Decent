using Unity.VisualScripting;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public CameraController cameraController;
    public AudioSource bossMusic;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraController.EnterBossRoom();

            if (bossMusic != null)
            {
                bossMusic.Play();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraController.ExitBossRoom();

            if (bossMusic != null)
            {
                bossMusic.Stop();
            }
        }
    }
}
