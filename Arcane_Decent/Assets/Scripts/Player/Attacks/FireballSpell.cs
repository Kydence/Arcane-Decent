// Attach to player, handles key input + spawning fireball

using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab; // Fireball object
    [SerializeField] private Transform spawnPoint;      // Where it appears (hand/staff)
    [SerializeField] private float direction = 1f;      // 1 = right, -1 = left

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) // Fireball on X
        {
            GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);

            FireballProjectile proj = fireball.GetComponent<FireballProjectile>();
            if (proj != null)
            {
                proj.SetDirection(direction);
            }
        }
    }
}