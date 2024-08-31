using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeLineStarGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    
    //[SerializeField] TextMeshProUGUI TimerText;
    float elapsedTime;
    
    [SerializeField] float remainingTime;
    
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        
    }
    
    void Update()
    {
        #region <นับเวลาเดินหน้า>
        /*
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); */
       
        
        #endregion
        
        #region <จำเวลานับถอยหลัง>

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            // gameOver
            PauseGame();
            timerText.color = Color.red;
            
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        #endregion
        
        
    }
    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    
}
