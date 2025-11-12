using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public AudioClip checkpointSound; // sound played when picking up new checkpoint
    Transform currentCheckpoint; // store last checkpoint here
    [SerializeField] private Transform defaultSpawn;

    private Health playerHealth;

    void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        // if we have a checkpoint, use it; otherwise, use a default spawn position
        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position; // move player to checkpoint position
        }
        else if (defaultSpawn != null)
        {
            transform.position = defaultSpawn.position; // move player to default spawn position
        }
        
        // Restore player health and reset animation
        playerHealth.Respawn(); //Restore player health and reset animation
    }

    // Activate Checkpoints
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // store checkpoint just activated as current one
            // SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; // deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("Appear"); // trigger checkpoint animation
        }
    }
}
