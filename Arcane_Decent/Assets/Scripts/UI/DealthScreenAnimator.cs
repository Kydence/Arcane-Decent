using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
public Texture2D[] frames;
public int framesPerSecond = 10;
private Renderer rend;

void Start()
{
rend = GetComponent<Renderer>();
}

void Update()
{
int index = (int)(Time.time * framesPerSecond) % frames.Length;
rend.material.mainTexture = frames[index];
}
}
