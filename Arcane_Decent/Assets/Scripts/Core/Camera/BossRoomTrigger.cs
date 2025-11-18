using UnityEngine;
public class BossRoomTrigger : MonoBehaviour
{
    public CameraController cameraController;
    public AudioSource bossMusic;
    public GameObject bosshealth;
    public GameObject boss;
    public int init = 0;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraController.EnterBossRoom();

            if (bossMusic != null)
            {
                init = 1;
                bossMusic.Play();
                bosshealth.SetActive(true);
                boss.SetActive(true);
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
