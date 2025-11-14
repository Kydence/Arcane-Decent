using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    public AudioClip checkpointSound; // sound played when picking up new checkpoint
    Transform currentCheckpoint; // store last checkpoint here
    [SerializeField] private Transform defaultSpawn;

    private Health playerHealth;
    [SerializeField] private Button bspawn;
    [SerializeField] private Button bquit;

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
        playerHealth.Respawn(); 
        
       
    }

    // Activate Checkpoints
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // store checkpoint just activated as current one
            CloseSoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; // deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("Appear"); // trigger checkpoint animation
        }
    }
   
}
