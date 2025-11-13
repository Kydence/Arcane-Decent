using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Windows.Speech;

public class MagicArrowProjectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed;
    private bool hit;
    private float direction;

    private BoxCollider2D boxCollider;
    private float lifetime;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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

        // Actually hits
        hit = true;
        boxCollider.enabled = false;
        Deactive();
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
    public void SetDirection(float _ditection)
    {
        lifetime = 0;
        direction = _ditection;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _ditection)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }
    private void Deactive()
    {
        gameObject.SetActive(false);
    }
}
