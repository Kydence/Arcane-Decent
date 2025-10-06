using Unity.VisualScripting;
using UnityEngine;

public class BasicAttacka : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
            Attack();
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
            if (arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
