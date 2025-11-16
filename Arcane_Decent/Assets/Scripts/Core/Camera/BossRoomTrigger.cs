using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    public CameraController cameraController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraController.EnterBossRoom();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraController.ExitBossRoom();
        }
    }
}
