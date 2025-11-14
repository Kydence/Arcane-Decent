using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Follow")]
    public Transform player;
    [SerializeField] private float disY;
    [SerializeField] private float disX;
    
    void Update()
    {
        transform.position = new Vector3(player.position.x+disX, player.position.y+disY, transform.position.z);
    }
}
