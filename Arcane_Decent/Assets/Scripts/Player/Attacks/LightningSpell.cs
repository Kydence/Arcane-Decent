using UnityEngine;

public class LightningSpell : MonoBehaviour
{
    [SerializeField] private GameObject lightningPrefab;   // The visual (beam, bolt, etc.)
    [SerializeField] private Transform spawnPoint;          // Where it appears (usually player's hand)
    [SerializeField] private float duration = 0.1f;         // Pulse duration for visual refresh

    private GameObject activeLightning;
    private float timer;

    private void Update()
    {
        // When player holds down C — start or keep lightning active
        if (Input.GetKey(KeyCode.C))
        {
            if (activeLightning == null)
            {
                // Spawn lightning when key is first pressed
                activeLightning = Instantiate(lightningPrefab, spawnPoint.position, spawnPoint.rotation);
            }

            // Keep it positioned/aimed with player if needed
            if (activeLightning != null)
                activeLightning.transform.position = spawnPoint.position;

            // Optionally refresh visual every short interval (for flicker or continuous strike)
            timer += Time.deltaTime;
            if (timer >= duration)
            {
                timer = 0f;
                // You could respawn or reset particle system here
            }
        }
        else
        {
            // When player releases C — stop the spell
            if (activeLightning != null)
            {
                Destroy(activeLightning);
                activeLightning = null;
            }
        }
    }
}