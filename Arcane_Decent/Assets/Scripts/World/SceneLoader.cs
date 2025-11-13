using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] string levelname;
    private CircleCollider2D body;
    void Awake()
    {
        body = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            print("next level");
            SceneManager.LoadScene(levelname);
        }
    }
}
