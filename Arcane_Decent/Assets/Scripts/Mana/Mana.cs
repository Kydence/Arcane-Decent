using System.Threading;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [Header("Mana")]

    [SerializeField] private float startingMana;
    public float currentMana { get; private set; }

    private SpriteRenderer spriteRend;
    private float ManaTimer = 0;

    private void Awake()
    {
        currentMana = startingMana;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void UseMana(float _amount)
    {
        currentMana = Mathf.Clamp(currentMana - _amount, 0, startingMana);
    }

    public void restoreMana(float _amount)
    {
        currentMana = Mathf.Clamp(currentMana + _amount, 0, startingMana);
    }

    private void Update()
    {
        ManaTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J))
        {
            UseMana(1);
            ManaTimer = 0;
        }
        else if(!Input.GetMouseButtonDown(0) && ManaTimer >= 2)
        {
            restoreMana(1);
            ManaTimer = 0;
        }
    }
}
