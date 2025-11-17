using UnityEngine;

public class Special1Collision : MonoBehaviour
{
   [SerializeField] private BoxCollider2D box;
   public bool gothim = false;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            gothim = true;
           
        }
        
       
    }
     void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            gothim = true;
           
        }
        
       
    }

       void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gothim = false;
           
        }
    }
}
