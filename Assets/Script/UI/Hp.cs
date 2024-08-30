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

    //private AudioManager audioManager;

    private void Awake()
    {
       //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = 4;
       // gameOverPanel.SetActive(false); // ปิด panel สำหรับ game over เมื่อเริ่มเกม
    }

    // Update is called once per frame
    void Update()
    {
        if (hp >= 10)
        {
            //Time.timeScale = 0f; // หยุดเวลาในเกม
            //gameOverPanel.SetActive(true); // เปิด panel สำหรับ game over
            /*if (Input.GetKeyDown(KeyCode.M)) //God Mode
            {
                Time.timeScale = 0f; // หยุดเวลาในเกม
                PauseGame();
            }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hp" )
        {
            hp++;
            hpText.text = "Hp = " + hp;
            hpText.text = "Hp = " + hp;
            Destroy(other.gameObject);
            
            //audioManager.PlaySFX(audioManager.SFXSurce);
            
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
            Destroy(gameObject);
            PauseGame();
            //audioManager.PlaySFX(audioManager.death);
        }
    }
    void UpdateHpUI()
    {
        hpText.text = "Hp = " + hp;
    }
    
    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    
    
}