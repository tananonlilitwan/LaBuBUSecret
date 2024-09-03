using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject currentPausePanel; // Pause Panel อันเดิม
    [SerializeField] private GameObject heavenPausePanel;  // Pause Panel อันใหม่ 
    
    [SerializeField] private GameObject menuPanel;         // Panel 'menu'
    [SerializeField] private GameObject canvasPanel;       // Panel 'Canvas'
    
    //[SerializeField] private GameObject heavenPanel;

    /*void Update()
    {
        if (heavenPanel.activeSelf)
        {
            // รีเซ็ตเกมโดยการโหลด Scene ปัจจุบันอีกครั้ง
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }*/
    
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
        //SceneManager.LoadScene("menu");
        
        // ปิด Panel 'Heaven'
        if (heavenPausePanel != null)
        {
            heavenPausePanel.SetActive(false);
        }

        // เปิด Panel 'menu'
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }

        // รีเซ็ตเกม โดยโหลด Scene 'menu'
        SceneManager.LoadScene("MapScene");

        // ตั้งค่า Time.timeScale ให้เป็น 1 เพื่อให้เกมทำงานตามปกติ
        Time.timeScale = 1;
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
    
    // ฟังก์ชันนี้จะถูกเรียกเมื่อปุ่มถูกกด
    public void OnNextButtonClick()
    {
        // ปิด Pause Panel อันเดิม
        if (currentPausePanel != null)
        {
            currentPausePanel.SetActive(false);
        }

        // เปิด Pause Panel อันใหม่ (Heaven)
        if (heavenPausePanel != null)
        {
            heavenPausePanel.SetActive(true);
        }

        // หยุดเวลา
        Time.timeScale = 0;
    }
}
