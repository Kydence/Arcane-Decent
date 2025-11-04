using System;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float explosionDuration = 0.3f;

    private bool hit;
    private float direction;
    private float lifetime;

    private BoxCollider2D boxCollider;
    private Animator animator; //For explosion animation

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit)
        {
            return;
        }

        // Move fireball horizontally based on direction and speed
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5)
        {
            gameObject.SetActive(false);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;

        // Play explosion animation if exists
        if (animator != null)
        {
            animator.SetTrigger("explode");
        }
       

        // Deactivate after a brief explosion delay
        Invoke(nameof(Deactivate), explosionDuration);
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}