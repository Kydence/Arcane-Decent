using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public bool dead;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private GameObject deathscreen;
    [SerializeField] private GameObject Juice;
    [SerializeField] private GameObject bspawn;
    [SerializeField] private GameObject bquit;
    private Button spawnbutton;
    private bool wait = false;
    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if(bspawn != null){
        deathscreen.SetActive(false);
        spawnbutton = bspawn.GetComponent<Button>();
        //spawnbutton.onClick.AddListener(Respawn);
        bspawn.SetActive(false);
        bquit.SetActive(false);
        }
       

    }
    


    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            StartCoroutine(Invunerability());
            CloseSoundManager.instance.PlaySound(hurtSound);
            // hurt
        }
        else
        {
            //player dead
            if (!dead)
            {
                if (GetComponent<PlayerMovement>() != null)
                {
                    anim.SetTrigger("die");
                    CloseSoundManager.instance.PlaySound(deathSound);
                    GetComponent<PlayerMovement>().enabled = false;
                    if(bspawn != null){
                    Juice.SetActive(false);
                    deathscreen.SetActive(true);
                    
                    bspawn.SetActive(true);
                    bquit.SetActive(true);
                    
                    }

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
        if(bspawn != null){
        deathscreen.SetActive(false);
        Juice.SetActive(true);
        
        bspawn.SetActive(false);
        bquit.SetActive(false);

        GetComponent<PlayerMovement>().enabled = true;
        }
        
     
        
    }
   

    
    

}
