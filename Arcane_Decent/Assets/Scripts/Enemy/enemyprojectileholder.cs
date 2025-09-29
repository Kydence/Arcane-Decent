using UnityEngine;

public class enemyprojectileholder : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    void Update()
    {
        transform.localScale = -enemy.localScale;
    }
}
