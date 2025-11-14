using UnityEngine;

public class FireBallAttack : MonoBehaviour
{
 [SerializeField] public float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    public float cooldownTimer = Mathf.Infinity;
    [SerializeField] private Mana mana;
    [SerializeField] private KeyCode button;
    [SerializeField] private AudioClip fireballAttackSound;
    private bool fireballUnlocked;

    private void Awake()
    {

    }

    private void Start()
    {
        if (PowerupState.instance != null && PowerupState.instance.hasFireball)
        {
            fireballUnlocked = true;
        }
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (!fireballUnlocked)
            return;
        
        if (Input.GetKeyDown(button) && cooldownTimer > attackCooldown)
            if (mana.currentMana > 0 && mana.currentMana/5>=1)
            {
                Attack();
            }
    }

    public void UnlockFireball()
    {
        fireballUnlocked = true;
    }

    private void Attack()
    {
        CloseSoundManager.instance.PlaySound(fireballAttackSound);
        cooldownTimer = 0;
        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<FireballProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
