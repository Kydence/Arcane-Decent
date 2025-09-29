using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCoolDown;
    
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectile;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Enemypatrol enemypatrol;

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

                //attack
                RangedAttack();
            }
        }
        if (enemypatrol != null)
        {
            enemypatrol.enabled = !PlayerInsight();
        }
    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        //shoot projectile
        projectile[FindProjectile()].transform.position = firePoint.position;
        projectile[FindProjectile()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindProjectile()
    {
        for (int i = 0; i < projectile.Length; i++)
        {
            if (!projectile[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    
    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);


        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
