using UnityEngine;

public class FallingItem : MonoBehaviour
{
       [SerializeField] private float speed;
    [SerializeField] private float range;
    
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform point;
    private Vector3 destination;
    private Rigidbody2D body;

    private float checkTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        CheckForPlayer();
        
    }

    private void CheckForPlayer()
    {

        Debug.DrawRay(transform.position, -transform.up * range, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, range, playerLayer);

        if (hit.collider != null)
        {
          
            body.gravityScale = 1;
            checkTimer = 0;
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
