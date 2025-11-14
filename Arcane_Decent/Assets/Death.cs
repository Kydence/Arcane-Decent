using UnityEngine;

public class Death : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator deathscreen;
 void Die()
    {
        deathscreen.Play("Death");
    }
}
