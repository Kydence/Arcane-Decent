using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private Health playerHealth;
    private Enemypatrol enemypatrol;

    //NEED TO HAVE ANIMATION EVENT TO MAKE SURE MELEE ENEMY DOESN'T ATTACK WHEN PLAYER IS IN THE 
    //ENEMY HIT BOX AND SO THE PLAYER IS ABLE TO DODGE THE ATTACK
    void Awake()
    {
        enemypatrol = GetComponentInParent<Enemypatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        //attack only when player in sight
        
        if (PlayerInsight())
        {
            
            if (cooldownTimer >= attackCoolDown)
            {
                cooldownTimer = 0;
                DamagePlayer();
                //attack
            }
        }
       if (enemypatrol != null)
        {
            enemypatrol.enabled = !PlayerInsight();
        }
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //if the player is in the hit box they take damage
        if (PlayerInsight())
        {
            playerHealth.TakeDamage(damage);
            //damge player health
        }
    }
   
}
