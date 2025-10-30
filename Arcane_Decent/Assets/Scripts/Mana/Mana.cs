using System.Threading;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [Header("Mana")]

    [SerializeField] private float startingMana;
   
    public float currentMana { get; private set; }

    //private SpriteRenderer spriteRend;
    private float attackCooldown;
    private float cooldownTimer;
    public float ManaTimer = 0;
    
    private void Awake()
    {
        //player has full mana when getting in the game
        currentMana = startingMana;
        // spriteRend = GetComponent<SpriteRenderer>();

        //gets the attack cooldown from basicattack()
        attackCooldown = GetComponent<BasicAttacka>().attackCooldown;
        //gets the starting cooldown value from basicarrack()
        cooldownTimer = GetComponent<BasicAttacka>().cooldownTimer;
    }

    //subtracts _amount from the currentMana float
    public void UseMana(float _amount)
    {
        currentMana = Mathf.Clamp(currentMana - _amount, 0, startingMana);
    }

    //adds _amount to the currentMana float
    public void restoreMana(float _amount)
    {
        currentMana = Mathf.Clamp(currentMana + _amount, 0, startingMana);
    }

    private void Update()
    {
        print(cooldownTimer);
        
        ManaTimer += Time.deltaTime;
        cooldownTimer += Time.deltaTime;
        //checks if the player uses a basic attack and that the AttackCoolDown is over
        if (Input.GetKeyDown(KeyCode.J) && cooldownTimer > attackCooldown)
        {
            UseMana(1);
            ManaTimer = 0;
            cooldownTimer = 0;
        }
        //restores mana
        else if (ManaTimer >= 2)
        {
            restoreMana(1);
            ManaTimer = 0;
        }
    }
}
