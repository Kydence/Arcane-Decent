using UnityEngine;

public class Bossmoves : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;
    private float SpecialdownTimer;
    private Health playerHealth;
    private BossPatrol enemypatrol;
    private Animator anim;
    public int random;
    [SerializeField] private float SpecialCoolDown;
    private Transform player;
    private Transform enemy;
    private bool special = false;
    
    
    private bool upbox;
    

    //NEED TO HAVE ANIMATION EVENT TO MAKE SURE MELEE ENEMY DOESN'T ATTACK WHEN PLAYER IS IN THE 
    //ENEMY HIT BOX AND SO THE PLAYER IS ABLE TO DODGE THE ATTACK
    void Awake()
    {
        enemypatrol = GetComponentInParent<BossPatrol>();
        player = GetComponentInParent<BossPatrol>().player;
        enemy= GetComponentInParent<BossPatrol>().enemy;
        
         anim = GetComponent<Animator>();
         SpecialdownTimer = 0;
    }

    private void Update()
    {
        upbox = GetComponentInChildren<Special1Collision>().gothim;
        cooldownTimer += Time.deltaTime;
        SpecialdownTimer+= Time.deltaTime;
        
        //attack only when player in sight
        random = Random.Range(1,20);
        print(random);
        if(random == 1 && SpecialdownTimer>= SpecialCoolDown)
        {
              enemy.position = new Vector3(player.position.x, enemy.position.y, enemy.position.z);
              special = true;
              anim.SetTrigger("Special1");
              cooldownTimer =0;
              SpecialdownTimer=0;
            
              
        }
        else{
        
        if (PlayerInsight())
        {
            
            if (cooldownTimer >= attackCoolDown) //&& playerHealth.currentHealth > 0)
            {
               
                print("attack");
                anim.SetBool("isWalk",false);
                 anim.SetTrigger("attack");
                cooldownTimer = 0;
               
                //DamagePlayer();
                //attack
            }
        }
       if (enemypatrol != null)
        {
            enemypatrol.enabled = !PlayerInsight();
            
        }
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
    private void SpecialDamge()
    {
        if(upbox){
         playerHealth = player.GetComponent<Health>();
         playerHealth.TakeDamage(damage);
        }
       
        
    }
    private void KILL()
    {
        Destroy(gameObject);
    }
    

}
