using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [Header("Follow")]
    public Transform player;
    public float normalOrthoSize = 5f;
    [SerializeField] private float disY;
    //[SerializeField] private float disX;

    [Header("Boss Room")]
    public Transform bossTopLeft;
    public Transform bossBottomRight;

    Camera cam;
    bool inBossRoom;
    Vector3 bossCenter;
    float bossOrthoSize;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        // Normal camera size
        cam.orthographicSize = normalOrthoSize;

        // Precompute boss-room camera center and size if markers are set
        if (bossTopLeft != null && bossBottomRight != null)
        {
            ComputeBossRoomCamera();
        }
    }

    void Update()
    {
        //transform.position = new Vector3(player.position.x+disX, player.position.y+disY, transform.position.z);

        if (inBossRoom)
        {
            // Lock camera to boss room center
            transform.position = new Vector3(bossCenter.x, bossCenter.y, transform.position.z);
        }
        else
        {
            // Normal follow
            transform.position = new Vector3(player.position.x, player.position.y+disY, transform.position.z);
        }
    }

    void ComputeBossRoomCamera()
    {
        // Room bounds
        float roomWidth = Math.Abs(bossBottomRight.position.x - bossTopLeft.position.x);
        float roomHeight = Math.Abs(bossTopLeft.position.y - bossBottomRight.position.y);

        // Center point
        bossCenter = (bossTopLeft.position + bossBottomRight.position) / 2f;

        // Orthographic camera's size = half of the visible height in world units
        // Both width and height must fit
        float sizeForHeight = roomHeight / 2;
        float sizeForWidth = roomWidth / (2f * cam.aspect);

        bossOrthoSize = Mathf.Max(sizeForHeight, sizeForWidth);
    }

    // Called by trigger when exiting or entering boss room
    public void EnterBossRoom()
    {
        if (!inBossRoom)
        {
            inBossRoom = true;
            cam.orthographicSize = bossOrthoSize;
        }
    }

    public void ExitBossRoom()
    {
        if (inBossRoom)
        {
            inBossRoom = false;
            cam.orthographicSize = normalOrthoSize;
        }
    }
}
