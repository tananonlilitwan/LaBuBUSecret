using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Help()
    {
        SceneManager.LoadScene("UIhelp 0");
    }

    public void Quit()
    {
        SceneManager.LoadScene("UIScene");
    }

    public void Back()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Next()
    {
        SceneManager.LoadScene("UIEndCredit");
    }

    public void Next0()
    {
        SceneManager.LoadScene("UIhelp 1");
    }
    
    public void Next1()
    {
        SceneManager.LoadSceneAsync(0);
    }
    
    public void Next2()
    {
        SceneManager.LoadScene("UIhelp 2");
    }
    
    public void Next3()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }

    public void Skip()
    {
        SceneManager.LoadScene("UIScene");
    }
    
    public void Next4()
    {
        SceneManager.LoadScene("UIEndCredit");
    }
}
