using System;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    //[SerializeField] private float explosionDuration = 0.3f;

    private bool hit;
    private float direction;
    private float lifetime;

    private BoxCollider2D boxCollider;
    // private Animator animator; For explosion animation

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        // animator = GetComponent<Animator>();
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
        if (lifetime > 5f)
        {
            gameObject.SetActive(false);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Play explosion animation if exists
        /*
        if (animator != null)
        {
            animator.SetTrigger("explode");
        }
        

        // Deactivate after a brief explosion delay
        Invoke(nameof(Deactivate), explosionDuration);
        */

        // Ignore Player
        if (collision.CompareTag("Player"))
        {
            return;
        }

        // Ignore the FarSoundZone trigger
        if (collision.GetComponent<FarSoundZone>())
        {
            return;
        }

        // Ignore BossRoomTrigger
        if (collision.GetComponent<BossRoomTrigger>())
        {
            return;
        }

        // Actually hits
        hit = true;
        boxCollider.enabled = false;
        gameObject.SetActive(false);
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0f;
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

    /*
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    */
}