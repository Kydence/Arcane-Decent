using UnityEngine;

public class FireballPickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;

    void Awake()
    {
        // if already unlocked from previous scene, hide pickup
        if (PowerupState.instance != null && PowerupState.instance.hasFireball)
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
            PowerupState.instance.hasFireball = true;
        }

        // enable fireball on this player instance
        FireBallAttack fire = other.GetComponent<FireBallAttack>();
        if (fire != null)
        {
            fire.UnlockFireball();
        }

        if (pickupSound != null && CloseSoundManager.instance != null)
        {
            CloseSoundManager.instance.PlaySound(pickupSound);
        }

        gameObject.SetActive(false);
    }
}
