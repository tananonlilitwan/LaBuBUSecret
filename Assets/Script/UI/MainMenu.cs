using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;         // Panel 'menu'
    [SerializeField] private GameObject canvasPanel;       // Panel 'Canvas'

    [SerializeField] private GameObject heavenPanel; // Panel 'heaven'
    
    [SerializeField] private GameObject howToPlayPanel;  // Panel 'HoeToPlay'
    
    
    
    private AudioManager audioManager; // เสียงในเกม
    
    private void Start()
    {
        // หยุดเกมเมื่อเริ่มต้น (จนกว่าจะกดปุ่ม Play)
        Time.timeScale = 0;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม

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
            
            if (menuPanel.activeSelf) // ถ้า Panel ถูกเปิดอยู่
            {
                audioManager.StopBackgroundMusic(); // หยุดเสียงเกมโดยใช้ฟังก์ชันของ AudioManager
            }
            else
            {
                audioManager.PlayBackgroundMusic(); // เล่นเสียงเกม
            }

            // สลับการแสดงผลของ Panel
            //menuPanel.SetActive(!menuPanel.activeSelf);
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

    public void HowToPlay()
    {
        // ตรวจสอบว่า Panel 'menu' ไม่เป็นค่าว่างและปิดมัน
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);  // ปิด Panel เมนู
            audioManager.PlaySFX(audioManager.paper); // เสียงSFX กระดาษ
        }

        // ตรวจสอบว่า Panel 'HowToPlay' ไม่เป็นค่าว่างและเปิดมัน
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(true);  // เปิด Panel "How To Play"
            audioManager.PlaySFX(audioManager.paper); // เสียงSFX กระดาษ
        }
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
    
    public void StopSFX(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    
    
}
