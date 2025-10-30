using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicAttacka : MonoBehaviour
{
    [SerializeField] public float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    public float cooldownTimer = Mathf.Infinity;
    [SerializeField] private Mana mana;
    [SerializeField] private KeyCode button;
    
    

    private void Awake()
    {

    }

    private void Update()
    {
        
        if (Input.GetKeyDown(button) && cooldownTimer > attackCooldown)
            if (mana.currentMana > 0)
            {
                Attack();
            }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        cooldownTimer = 0;
        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<MagicArrowProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
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
