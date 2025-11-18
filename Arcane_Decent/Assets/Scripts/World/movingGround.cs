using UnityEngine;

public class movingGround : MonoBehaviour
{
    [SerializeField] private float movingDistance;
    [SerializeField] private Transform ground;
    [SerializeField] private float speed;
    [SerializeField] private int direction;
    [SerializeField] private bool goback;
   
    private float thing;
    private bool begin = false;
    private bool back = false;
    private Vector3 initScale;
    void Awake()
    {
        initScale = ground.localScale;
        thing = ground.position.x;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {

        if (begin)
        {
            if (direction > 0)
            {
                if (ground.position.x < thing + movingDistance)
                {
                    MoveInDirection(direction);
                }

            }
            else
            {
                if (ground.position.x > thing + movingDistance)
                {
                    if (ground.position.x > initScale.x)
                    {
                        MoveInDirection(direction);
                    }
                }
            }
        }
        if (goback == true) {
            if(!begin)
            {
                if (direction > 0)
                {
                    if (ground.position.x < thing + movingDistance)
                    {
                        if (ground.position.x > thing)
                        {
                            MoveInDirection(-direction);
                        }
                    }

                }
                else
                {
                    if (ground.position.x > thing + movingDistance)
                    {
                        if (ground.position.x < thing)
                        {
                            MoveInDirection(-direction);
                        }
                    }
                }
            }
        }

    }
    private void MoveInDirection(int _direction)
    {

        //Make Enemy face direction
        //ground.localScale = new Vector2(Mathf.Abs(initScale.x) * _direction, initScale.y);
        //Move in that direciton
        ground.position = new Vector3(ground.position.x + Time.deltaTime * _direction * speed, ground.position.y, ground.position.z);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            begin = true;
           // back = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && goback == true)
        {
            //back = true;
            begin = false;
        }
    }
}
