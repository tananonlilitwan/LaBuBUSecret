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
    
    [SerializeField] private GameObject atxPrefab; // Prefab ของ PowerUpAtx ที่จะหมุนรอบตัว Player
    [SerializeField] private float rotationSpeed = 100f; // ความเร็วในการหมุนรอบตัว Player
    [SerializeField] private float orbitRadius = 2f; // ระยะห่างระหว่าง Atx และ Player
    private GameObject activePowerUpAtx1;
    private GameObject activePowerUpAtx2;
    private Transform playerTransform;
    
    
    [SerializeField] private GameObject Spearfab; // Prefab ของ PowerUpSpear ยิงเร็วขึ้น
    [SerializeField] private float powerUpFireRate = 0.5f;  // ความเร็วในการยิงเมื่อใช้ Spearfab
    // ประกาศตัวแปร isPowerUpActive เพื่อตรวจสอบว่าผู้เล่นกำลังอยู่ในสถานะ PowerUp หรือไม่
    private bool isPowerUpActive = false;
    // ประกาศตัวแปรที่หายไปอื่นๆ
    public GameObject defaultBulletPrefab;
    public float defaultFireRate = 2f;
    public Transform firePoint;
    private GameObject currentBulletPrefab;
    private float currentFireRate;
    private float shootingTimer;
    private Transform player; // ประกาศตัวแปร player

    
    

    
    
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake() //Start()
    {
        // เริ่มต้น Coroutine ที่จะยิงกระสุนอัตโนมัติ
        StartCoroutine(AutoFire());
        
        
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
        
         playerTransform = transform; // เก็บตำแหน่งของ Player
         
         currentFireRate = defaultFireRate;
         currentBulletPrefab = defaultBulletPrefab;
         shootingTimer = 0f;
         player = GameObject.FindGameObjectWithTag("Player").transform; // หาผู้เล่นในเกม
    }
    

    private void Update()
    {
        // ถ้า Atx เปิดอยู่ ให้หมุนรอบตัว Player
        if (activePowerUpAtx1 != null && activePowerUpAtx2 != null)
        {
            // หมุน PowerUp รอบ Player
            activePowerUpAtx1.transform.RotateAround(playerTransform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
            activePowerUpAtx2.transform.RotateAround(playerTransform.position, Vector3.forward, -rotationSpeed * Time.deltaTime);

            // อัพเดตตำแหน่งของ PowerUp ให้คงระยะห่างจาก Player
            Vector3 offset1 = (activePowerUpAtx1.transform.position - playerTransform.position).normalized * orbitRadius;
            activePowerUpAtx1.transform.position = playerTransform.position + offset1;
            
            Vector3 offset2 = (activePowerUpAtx2.transform.position - playerTransform.position).normalized * orbitRadius;
            activePowerUpAtx2.transform.position = playerTransform.position + offset2;
        }
        
        // ควบคุมการยิงกระสุนตามความเร็วการยิง
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0)
        {
            Shoot();
            shootingTimer = currentFireRate;  // รีเซ็ต timer ตามความเร็วการยิงปัจจุบัน
        }

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
        
        if (other.CompareTag("PowerUpAtx"))
        {
            // เก็บ PowerUp และทำลายวัตถุเดิม
            Destroy(other.gameObject);

            // สร้าง Atx ให้หมุนรอบ Player
            if (activePowerUpAtx1 == null) 
            {
                Vector3 initialPosition1 = playerTransform.position + new Vector3(orbitRadius, 0, 0);
                activePowerUpAtx1 = Instantiate(atxPrefab, initialPosition1, Quaternion.identity);

                Vector3 initialPosition2 = playerTransform.position + new Vector3(-orbitRadius, 0, 0);
                activePowerUpAtx2 = Instantiate(atxPrefab, initialPosition2, Quaternion.identity);

                Debug.Log("Atx Created at: " + initialPosition1 + " and " + initialPosition2);
            }

            // เริ่มการหมุน
            StartCoroutine(ActivateDoubleShot(50f)); //หมุน15วินาที 

            // เล่นเสียง SFX
            audioManager.PlaySFX(audioManager.Hp);
        }
        
        if (other.CompareTag("Spear"))
        {
            Destroy(other.gameObject);  // ทำลาย PowerUp หลังจากเก็บแล้ว
            StartCoroutine(ActivateDoubleShot(10f));  // เปิดใช้งานโหมดยิงเร็วขึ้นเป็นเวลา 10 วินาที
        
            audioManager.PlaySFX(audioManager.Hp);  // เล่นเสียง SFX เมื่อเก็บ PowerUp
        }
        
        
    }

    // Coroutine สำหรับเปิดใช้งานโหมดยิงสองทิศทางเป็นเวลา 10 วินาที
    private IEnumerator ActivateDoubleShot(float duration)
    {
        doubleShot = true; // เปิดโหมด Double Shot
        yield return new WaitForSeconds(duration); // รอเป็นเวลา 10 วินาที
        doubleShot = false; // ปิดโหมด Double Shot
        
        
            // ทำลาย Atx ที่หมุนหลังจากหมดเวลา
        if (activePowerUpAtx1 != null)
        {
            Destroy(activePowerUpAtx1);
        }
        if (activePowerUpAtx2 != null)
        {
            Destroy(activePowerUpAtx2);
        }
        
        
        isPowerUpActive = true;  // ตั้งค่าให้สถานะ PowerUp เป็น true
        currentFireRate = 0.5f;  // ตัวอย่าง: เพิ่มความเร็วในการยิง
        yield return new WaitForSeconds(duration);  // รอจนกระทั่งหมดเวลา
        isPowerUpActive = false;  // รีเซ็ตสถานะ PowerUp กลับเป็นปกติ
        currentFireRate = defaultFireRate;  // คืนค่า fireRate กลับเป็นค่าเดิม
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
    
    void Shoot()
    {
        if (currentBulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(currentBulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                Vector2 direction = (firePoint.right);
                bulletRb.velocity = direction * 10f;
            }
        }
    }
    
}

