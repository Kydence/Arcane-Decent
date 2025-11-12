using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private Animator anim;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberofFlashes;
    private SpriteRenderer spriteRend;
    private bool dead;
    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            StartCoroutine(Invunerability());
            //player hurt
        }
        else
        {
            //player dead
            if (!dead)
            {

                
                if (GetComponent<PlayerMovement>() != null)
                {
                    anim.SetTrigger("die");
                    GetComponent<PlayerMovement>().enabled = false;
                    
                }


                //Enemy
                if (GetComponentInParent<Enemypatrol>() != null)
                {

                    GetComponentInParent<Enemypatrol>().enabled = false;
                      Destroy(gameObject);
                }

                if (GetComponentInParent<MeleeEnemy>() != null)
                {
                    
                    GetComponent<MeleeEnemy>().enabled = false;
                }
                if(GetComponent<RangedEnemy>() != null)
                {
                    GetComponent<RangedEnemy>().enabled = false;
                  
                }
            dead = true;
            }
        }
    }
    private IEnumerator Invunerability()
    {
        if (currentHealth > 0)
        {
            Physics2D.IgnoreLayerCollision(3, 8, true);
            //invunerability duration
            for (int i = 0; i < numberofFlashes; i++)
            {
                spriteRend.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberofFlashes * 2));
                spriteRend.color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberofFlashes * 2));
            }
            Physics2D.IgnoreLayerCollision(3, 8, false);
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    
    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");

        GetComponent<PlayerMovement>().enabled = true;
    }
}
