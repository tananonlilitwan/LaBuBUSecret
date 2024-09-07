using System;
using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float fireRate; // กำหนดความถี่ในการยิง (เช่น ยิงทุกๆ 2 วินาที)
    [SerializeField] private Rigidbody2D rbbulletprefab;
    [SerializeField] private GameObject PosShool;
    [SerializeField] private Transform ShootPoint;
    
    private bool doubleShot = false; // ตัวแปรเก็บสถานะการยิงสองทิศทาง
    
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake() //Start()
    {
        // เริ่มต้น Coroutine ที่จะยิงกระสุนอัตโนมัติ
        StartCoroutine(AutoFire());
        
        
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
    }
    

    private void Update()
    {
        
    }

    // ฟังก์ชันยิงกระสุนอัตโนมัติ
    private IEnumerator AutoFire()
    {
        while (true)
        {
            // ยิงกระสุนอัตโนมัติทุกๆ fireRate วินาที
            yield return new WaitForSeconds(fireRate);
            
            // เรียกฟังก์ชันยิงกระสุน
            Fire();
            
            audioManager.PlaySFX(audioManager.shoot); // เสียง SFX  ยิงกระสุน
        }
    }
    
    // ฟังก์ชันยิงกระสุน
    private void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.magenta, 10f);

        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit2D.collider != null)
        {
            // ย้าย PosShool ไปยังตำแหน่งที่ตรวจจับด้วย Raycast
            PosShool.transform.position = new Vector2(hit2D.point.x, hit2D.point.y);

            // คำนวณทิศทางการยิงไปตามเมาส์
            Vector2 projectileDirection = CalculateProjectileVelocity(ShootPoint.position, hit2D.point, 1f);

            // ยิงกระสุนในทิศทางเมาส์
            FireInDirection(projectileDirection);

            if (doubleShot)
            {
                // หากอยู่ในโหมดยิงสองทิศทาง ยิงกระสุนอีกสองทิศทางที่ทำมุม 45 องศากับกระสุนหลัก
                Vector2 directionLeft = RotateVector(projectileDirection, 45);  // หมุน 45 องศาไปทางซ้าย
                Vector2 directionRight = RotateVector(projectileDirection, -45); // หมุน 45 องศาไปทางขวา
            
                FireInDirection(directionLeft);  // ยิงกระสุนในทิศทางที่หมุนไปทางซ้าย
                FireInDirection(directionRight); // ยิงกระสุนในทิศทางที่หมุนไปทางขวา
            }
        }
        
        
        
        // อันเเดิม
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.magenta, 10f);
        
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit2D.centroid != null)
        {
            // ย้าย PosShool ไปยังตำแหน่งที่ตรวจจับด้วย Raycast
            PosShool.transform.position = new Vector2(hit2D.point.x, hit2D.point.y);
            
            // คำนวณทิศทางการยิง
            Vector2 projectile = CalculateProjectileVelocity(ShootPoint.position, hit2D.point, 1f);
            
            // สร้างกระสุนและยิงไปในทิศทางที่คำนวณ
            Rigidbody2D fireBullet = Instantiate(rbbulletprefab, ShootPoint.position, Quaternion.identity);
            fireBullet.velocity = projectile;
        }*/
    }
    
    // ฟังก์ชันยิงกระสุนในทิศทางที่กำหนด new
    private void FireInDirection(Vector2 direction)
    {
        Rigidbody2D fireBullet = Instantiate(rbbulletprefab, ShootPoint.position, Quaternion.identity);
        fireBullet.velocity = direction;
    }
    
    // คำนวณความเร็วของกระสุน
    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float t)
    {
        Vector2 distance = target - origin;
        float distX = distance.x;
        float distY = distance.y;

        float velocityX = distX / t;
        float velocityY = distY / t + 0.5f * Mathf.Abs(Physics2D.gravity.y) * t;
        
        return new Vector2(velocityX, velocityY);
    }
    
    // ตรวจจับการชนกับ PowerUp
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject); // ทำลาย PowerUp หลังจากเก็บแล้ว
            StartCoroutine(ActivateDoubleShot(10f)); // เปิดโหมดยิงสองทิศทางเป็นเวลา 10 วินาที
            
            audioManager.PlaySFX(audioManager.Hp); // เสียง SFX Get PowerUp
        }
    }

    // Coroutine สำหรับเปิดใช้งานโหมดยิงสองทิศทางเป็นเวลา 10 วินาที
    private IEnumerator ActivateDoubleShot(float duration)
    {
        doubleShot = true; // เปิดโหมด Double Shot
        yield return new WaitForSeconds(duration); // รอเป็นเวลา 10 วินาที
        doubleShot = false; // ปิดโหมด Double Shot
    }
    
    
    // ฟังก์ชันสำหรับหมุนเวกเตอร์ new
    private Vector2 RotateVector(Vector2 vector, float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad; // แปลงองศาเป็นเรเดียน
        float cosAngle = Mathf.Cos(angleRadians);
        float sinAngle = Mathf.Sin(angleRadians);

        float rotatedX = vector.x * cosAngle - vector.y * sinAngle;
        float rotatedY = vector.x * sinAngle + vector.y * cosAngle;

        return new Vector2(rotatedX, rotatedY);
    }
    
}

