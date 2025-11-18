using UnityEngine;

public class SpikeNopass : MonoBehaviour
{
    [SerializeField] Collider2D player;
    [SerializeField] Collider2D wall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = player.GetComponent<Collider2D>();
        wall = wall.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(player, wall, true);
    }

  
}
