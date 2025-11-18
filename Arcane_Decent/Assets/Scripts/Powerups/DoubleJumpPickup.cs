using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;

    void Awake()
    {
        // if already unlocked from previous scene, hide pickup
        if (PowerupState.instance != null && PowerupState.instance.hasDoubleJump)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        // set global flag
        if (PowerupState.instance != null)
        {
            PowerupState.instance.hasDoubleJump = true;
        }

        // enable double jump on this player instance
        PlayerMovement movement = other.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.EnableDoubleJump();
        }

        if (pickupSound != null && CloseSoundManager.instance != null)
        {
            CloseSoundManager.instance.PlaySound(pickupSound);
        }

        gameObject.SetActive(false);
    }
}
