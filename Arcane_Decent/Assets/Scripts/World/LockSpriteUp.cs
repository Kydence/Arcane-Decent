using UnityEngine;

public class LockSpriteUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private SpriteRenderer thing;
    void Awake()
    {
         thing = GetComponent<SpriteRenderer>();
    }
    void LateUpdate()
    {
        thing.transform.rotation = Quaternion.identity;
    }
}
