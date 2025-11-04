using UnityEngine;

public class enemyprojectileholder : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    void Update()
    {
        if (enemy != null)
        {
            transform.localScale = enemy.localScale;
        }
    }
}
