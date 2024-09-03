using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;         // Panel 'menu'
    [SerializeField] private GameObject canvasPanel;       // Panel 'Canvas'

    [SerializeField] private GameObject heavenPanel; // Panel 'heaven'
    
    private void Start()
    {
        // หยุดเกมเมื่อเริ่มต้น (จนกว่าจะกดปุ่ม Play)
        Time.timeScale = 0;

        // แสดง Panel 'menu' เมื่อเริ่มต้น
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }

        // ซ่อน Panel 'Canvas' เมื่อเริ่มต้น
        if (canvasPanel != null)
        {
            canvasPanel.SetActive(false);
        }
        
        // ซ่อน Panel 'heaven' เมื่อเริ่มต้น
        if (heavenPanel != null)
        {
            heavenPanel.SetActive(false);
        }
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อกดปุ่ม Play
    public void Play()
    {
        // ปิด Panel 'menu'
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        // เปิด Panel 'Canvas'
        if (canvasPanel != null)
        {
            canvasPanel.SetActive(true);
        }
        
        // ปิด Panel 'menu'
        if (menuPanel != null)
        {
            heavenPanel.SetActive(false);
        }
        
        // ตั้งค่า Time.timeScale ให้เป็น 1 เพื่อให้เกมทำงานตามปกติ
        Time.timeScale = 1;
        
        
    }
    
    /*public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }*/
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("MapScene").completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation operation)
    {
        // รีเซ็ต Scene โดยโหลด Scene ปัจจุบันซ้ำ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Help()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    
    
}
