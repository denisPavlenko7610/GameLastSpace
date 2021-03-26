using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    private bool isPaused = false;

    private void Awake()
    {
        if (pausePanel && gameOverPanel)
        {
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
            }

            ShowPause(isPaused);
        }
    }

    public void ShowPause(bool isPause)
    {
        pausePanel.SetActive(isPause);
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}