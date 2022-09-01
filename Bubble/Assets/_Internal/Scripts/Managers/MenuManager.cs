using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PausePanel()
    {
        if(_pausePanel.activeInHierarchy == true)
        {
            _pausePanel.SetActive(false);
        }
        else
        {
            _pausePanel.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
