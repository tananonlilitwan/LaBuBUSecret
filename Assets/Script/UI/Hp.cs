using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Hp : MonoBehaviour
{
    
    public int hp;
    public Text hpText;
    [SerializeField] Text HpText;
    //public GameObject gameOverPanel;

    [SerializeField] private GameObject pausePanel;
    
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
       audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = 50;
        //gameOverPanel.SetActive(false); // ปิด panel สำหรับ game over เมื่อเริ่มเกม
        UpdateHpUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp >= 10) 
        {
            //Time.timeScale = 0f; // หยุดเวลาในเกม
            ////gameOverPanel.SetActive(true); // เปิด panel สำหรับ game over
            if (Input.GetKeyDown(KeyCode.M)) //God Mode
            {
                Time.timeScale = 0f; // หยุดเวลาในเกม
                PauseGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hp")
        {
            hp += 3; // เพิ่มค่า Hp 3 หน่วย
            hpText.text = "Hp = " + hp;
            Destroy(other.gameObject);

            audioManager.PlaySFX(audioManager.Hp); // เสียงSFX Get Hp
        }
        if (other.CompareTag("Enemy")) 
        {
            TakeDamage();
            //Destroy(other.gameObject); // ลบ player
        }
        if (other.gameObject.CompareTag("EnenmyBullet"))
        {
            TakeDamage();
            //Destroy(other.gameObject);
        }
    }
    void TakeDamage()
    {
        hp--;
        UpdateHpUI();
        
        if (hp <= 0)
        {
            //Destroy(gameObject);
            PauseGame();
            audioManager.PlaySFX(audioManager.End_Over_Enamy_Q_Player); // เสียง Player ตาย
            
            // เรียก Coroutine เพื่อรอเวลา 2 วินาทีแล้วเปิดเสียงที่สอง
            StartCoroutine(PlayCatSoundAfterDelay(4f)); // 2 วินาที
        }
    }
    IEnumerator PlayCatSoundAfterDelay(float delay) 
    {
        // รอเวลา delay วินาที
        yield return new WaitForSeconds(delay);

        // เล่นเสียงที่สอง (เสียงแมว)
        audioManager.PlaySFX(audioManager.cat);
    }
    void UpdateHpUI()
    {
        hpText.text = "Hp = " + hp;
    }
    
    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        
        if (pausePanel.activeSelf) // ถ้า Panel ถูกเปิดอยู่
        {
            audioManager.StopBackgroundMusic(); // หยุดเสียงเกมโดยใช้ฟังก์ชันของ AudioManager
        }
    }
    
    
}