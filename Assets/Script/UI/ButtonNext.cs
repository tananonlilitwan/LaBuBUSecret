using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNext : MonoBehaviour
{
    [SerializeField] private GameObject currentPausePanel; // Pause Panel อันเดิม
    [SerializeField] private GameObject heavenPausePanel;  // Pause Panel อันใหม่ (Heaven)

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
