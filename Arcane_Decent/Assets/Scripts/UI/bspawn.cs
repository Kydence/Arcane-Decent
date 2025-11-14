using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Bspawn : MonoBehaviour
{

  
    public void MainMenu()
    {
        SceneManager.LoadScene("_MainMenu");
    }
    public void newGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame()
    {
    #if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
    #else
    Application.Quit();
    #endif
    Debug.Log("Game is exiting");
    }

    public void Credits()
    {
        SceneManager.LoadScene("oCreditso");
    }
    
}