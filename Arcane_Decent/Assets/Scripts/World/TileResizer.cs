using UnityEngine;

public class TileResizer : MonoBehaviour
{
void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        bc.size = sr.size; // matches collider to sprite tiles
    }
}
