using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button5sec : MonoBehaviour
{
    [SerializeField] private Button buttonUI;

    void Start()
    {
        /// ตรวจสอบว่า buttonUI ได้ถูกตั้งค่าใน Inspector
        if (buttonUI != null)
        {
            // ปิดปุ่มไว้ก่อน
            buttonUI.gameObject.SetActive(false);

            // เริ่ม Coroutine เพื่อรอเวลา 2 วินาที แล้วแสดงปุ่ม
            StartCoroutine(ShowButtonAfterDelay(2f));
        }
        else
        {
            Debug.LogError("buttonUI is not assigned in the inspector.");
        }
    }

    IEnumerator ShowButtonAfterDelay(float delay)
    {
        // รอเวลา delay วินาที
        yield return new WaitForSeconds(delay);

        // เปิดใช้งานปุ่ม UI
        buttonUI.gameObject.SetActive(true);
    }
}
