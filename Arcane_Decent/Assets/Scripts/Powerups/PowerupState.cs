using UnityEngine;

public class PowerupState : MonoBehaviour
{
    public static PowerupState instance {get; private set;}

    [Header("Unlocked Powerups")]
    public bool hasFireball;
    public bool hasDoubleJump;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetAll()
    {
        hasFireball = false;
        hasDoubleJump = false;
    }
}
