using UnityEngine;

namespace Controller
{
    public class MenuController : MonoBehaviour
    {


        [SerializeField] private GameObject panelCredits;
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

        public void LoadScene(string sceneName)
        {
            FindObjectOfType<GameController>().StarNewGame(sceneName);
        }

        public void Quit()
        {
            FindObjectOfType<GameController>().CloseGame();
        }

    }
}
