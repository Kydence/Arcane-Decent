using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private SpriteRenderer spriteRend;

    private bool triggered; //when the trap getes triggered 
    private bool active; //when the trap is active and can hurt the player
    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
                //trigger the firetrap
                StartCoroutine(ActivateFiretrap());
            
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
            
        }

    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        spriteRend.color = Color.red; //turn the sprite red to notify the player

        //wait for delay
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; // turns the sprite back
        active = true;

        //Wait until Xseconds, deactive trap and reset trap
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        
    }
}
