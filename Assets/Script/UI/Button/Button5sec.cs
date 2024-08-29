using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button5sec : MonoBehaviour
{
    [SerializeField] private Button buttonUI;

    void Start()
    {
        // เริ่ม Coroutine เพื่อรอเวลา 5 วินาที แล้วเรียกใช้งานฟังก์ชัน ShowButton
        StartCoroutine(ShowButtonAfterDelay(2f));
    }

    IEnumerator ShowButtonAfterDelay(float delay)
    {
        // รอเวลา delay วินาที
        yield return new WaitForSeconds(delay);

        // เปิดใช้งานปุ่ม UI
        buttonUI.gameObject.SetActive(true);
    }
}
