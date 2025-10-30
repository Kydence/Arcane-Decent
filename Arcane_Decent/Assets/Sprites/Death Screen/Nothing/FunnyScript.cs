using NUnit.Framework.Constraints;
using UnityEngine;

public class FunnyScript : MonoBehaviour
{
    [SerializeField] private CircleCollider2D circle;
    [SerializeField] private GameObject funny;
    private SpriteRenderer jump;
    [SerializeField] private AudioSource thing;
    private bool done = false;
    private float end = 0;


    // Update is called once per frame
    void Start()
    {
        jump = funny.GetComponent<SpriteRenderer>();
        jump.enabled = false;
        funny.SetActive(false);
        end = 0;


    }
    void Update()
    {
        if(done == true)
        {
            
            float duration = thing.time;
            end += Time.deltaTime;
            if (end > duration)
            {
                funny.SetActive(false);
                jump.enabled = false;
                done = false;
                end = 0;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            //print("jump");

            funny.SetActive(true);
            jump.enabled = true;
            thing.Play();
           

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
            
        if(collision.tag == "Player"){
        done = true;
        }
       
        
    }
}
