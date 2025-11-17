using UnityEngine;

public class BreakGround : MonoBehaviour
{
   [SerializeField] Health boss;

    // Update is called once per frame
    void Update()
    {
        if(boss.currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
