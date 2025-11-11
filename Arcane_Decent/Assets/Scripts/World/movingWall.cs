using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class movingWall : MonoBehaviour
{
    [SerializeField] private float movingDistance;
    [SerializeField] private Transform ground;
    [SerializeField] private float speed;
    [SerializeField] private int direction;

    private float thing;
    private bool begin;
  

    void Awake()
    {
        thing = ground.position.y;
        begin = false;
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {

        if (begin)
        {
            if (direction > 0)
            {
                if (ground.position.y < thing + movingDistance)
                {
                    MoveInDirection(direction);
                }
            }
            else
            {
                if (ground.position.y > thing + movingDistance)
                {
                    MoveInDirection(direction);
                }
            }
        }

    }
    private void MoveInDirection(int _direction)
    {
        //Move in that direciton
        ground.position = new Vector3(ground.position.x, ground.position.y + Time.deltaTime * _direction * speed, ground.position.z);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            begin = true;
        }
    }

}
