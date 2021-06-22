using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject panelCredits;
    [SerializeField] private string sceneName;
    public void StarNewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ShowCredits()
    {
        if (!panelCredits.activeInHierarchy)
        {
            panelCredits.SetActive(true);
        }
        else
        {
            panelCredits.SetActive(false);
        }
    }
    
    public void CloseGame()
    { 
        Application.Quit();

    }
}
