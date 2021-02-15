using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        if (pausePanel && gameOverPanel )
        {
            pausePanel.SetActive(false);
            gameOverPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPause();
        }
    }

    public void ShowPause()
    {
        pausePanel.SetActive(true);
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
