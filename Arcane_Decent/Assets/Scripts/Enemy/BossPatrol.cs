using UnityEngine;

public class BossPatrol : MonoBehaviour
{

    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]

    [SerializeField] private Transform enemy;

    [Header("movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] private float idleDuration;
    private float idleTimer;
    
   [SerializeField] private Animator anim;

    [Header("boss specific stuff")]
   [SerializeField] private Transform player;
 

    private void Awake()
    {
        initScale = enemy.localScale;
       
    }

    void Update()
    {
        
        
        movingLeft = player.position.x < enemy.position.x;
        if (movingLeft)
        {
            if(enemy.position.x-8<= player.position.x)
            {
                anim.SetBool("isWalk",false);
            }
            else if ( enemy.position.x> player.position.x)
            {
                 MoveInDirection(-1);
            }  
            else
            {
              DirectionChange();
            }
         
        }
        else if(!movingLeft)
        {
             if(enemy.position.x+8>= player.position.x)
            {
                anim.SetBool("isWalk",false);
            }
            else if (enemy.position.x< player.position.x)
            {
               MoveInDirection(1);  
            }
            else
            {
                DirectionChange();
            }
      
        } 
        
    }

    private void DirectionChange()
    {
        anim.SetBool("isWalk",false);
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
           
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int _direction)
    {
        anim.SetBool("isWalk",true);
        idleTimer = 0;
        //Make Enemy face direction
        enemy.localScale = new Vector2(Mathf.Abs(initScale.x) * _direction, initScale.y);
        //Move in that direciton
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }
 



}
