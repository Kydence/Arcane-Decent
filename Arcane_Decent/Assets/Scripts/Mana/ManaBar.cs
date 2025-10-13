//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Mana playerMana;
    [SerializeField] private Image totalMana;
    [SerializeField] private Image currentMana;

 
    private void Start()
    {
        totalMana.fillAmount = playerMana.currentMana / 10;

    }
    private void Update()
    {
        currentMana.fillAmount = playerMana.currentMana / 10;
        
        
    }
}
