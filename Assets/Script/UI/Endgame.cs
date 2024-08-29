using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Endgame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
           // Win();
            PauseGame();
        }
    }

    /*private void Win()
    {
        winText.text = "Win";
    }*/

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    
}
