using UnityEngine;

public class BreakGround : MonoBehaviour
{
   [SerializeField] Health boss;
   [SerializeField] GameObject init;

    // Update is called once per frame
    void Update()
    {
        if(boss.currentHealth == 0 && init.GetComponent<BossRoomTrigger>().init == 1)
        {
            Destroy(gameObject);
        }
    }
}
