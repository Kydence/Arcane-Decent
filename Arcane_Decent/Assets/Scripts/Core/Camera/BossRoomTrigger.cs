using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BossRoomTrigger : MonoBehaviour
{
    public CameraController cameraController;
    public AudioSource bossMusic;
    public GameObject bosshealth;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraController.EnterBossRoom();

            if (bossMusic != null)
            {
                bossMusic.Play();
                bosshealth.SetActive(true);
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
                bosshealth.SetActive(false);
            }
        }
    }
}
