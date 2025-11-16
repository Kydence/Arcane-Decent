using UnityEngine;

public class Floating : MonoBehaviour
{
    [Header("Floating Settings")]
    public float amplitude = 0.25f; // how high it moves
    public float frequency = 1f; // how fast it moves

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
