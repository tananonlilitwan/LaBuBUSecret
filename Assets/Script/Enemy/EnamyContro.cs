using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnamyContro : MonoBehaviour
{
    private int hp;
    public Text hpText;
    //public GameObject bullet;
    //public Transform bulletPos;
    
    public Transform player;  // ตัวแปรสำหรับเก็บ Transform ของผู้เล่น
    public float moveSpeed = 2f;  // ความเร็วในการเคลื่อนที่
    public GameObject bulletPrefab;  // ตัวแปรสำหรับเก็บ Prefab ของกระสุน
    public Transform firePoint;  // ตำแหน่งที่กระสุนจะถูกยิงออกไป
    public float shootingInterval = 2f;  // ระยะเวลาการยิงกระสุน (วินาที)
    private float shootingTimer;
    
    private float timer;
    //private GameObject player;
    
    [SerializeField] private GameObject pausePanel;

    public LayerMask obstacleLayer;

    public GameObject[] itemDrops;
    
    void Start()
    {
        hp = 20; // Hp Robot
        //player = GameObject.FindGameObjectWithTag("Player");
        UpdateHpUI();
        
        // หาผู้เล่นโดยอัตโนมัติ
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player == null)
            {
                Debug.LogError("Player transform not found in the scene.");
            }
        }
        
        // ตรวจสอบว่าตัวแปรต่างๆ ถูกกำหนดค่าแล้ว
        if (player == null)
        {
            Debug.LogError("Player transform not assigned in EnamyContro.");
        }
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab not assigned in EnamyContro.");
        }
        if (firePoint == null)
        {
            Debug.LogError("Fire point not assigned in EnamyContro.");
        }
        shootingTimer = shootingInterval;  // ตั้งค่า timer เริ่มต้น
    }

    void Update()
    {
        if (player != null)
        {
            // คำนวณทิศทางไปยังผู้เล่น
            Vector2 direction = (player.position - transform.position).normalized;
            
            // คำนวณตำแหน่งถัดไปที่ศัตรูจะเคลื่อนที่ไป
            Vector2 newPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

            // ตรวจสอบว่าไม่มีสิ่งกีดขวางข้างหน้า
            if (!IsObstacleInWay(newPosition))
            {
                transform.position = newPosition;
            }
            
            // กำหนดตำแหน่งใหม่ให้กับศัตรู
            transform.position = newPosition;

            // จัดการเวลาในการยิง
            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0)
            {
                Shoot();
                shootingTimer = shootingInterval;  // รีเซ็ต timer
            }
            
            if (hp <= 0)
            {
                Time.timeScale = 0f;
            }
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            TakeDamage();
        } 
    }
    
    void TakeDamage()
    {
        hp--;
        UpdateHpUI();
        
        if (hp <= 0)
        {
            Destroy(gameObject);
            //PauseGame();
            
            // ถ้าEnamy ตาย Item จะ Dorp ออกมา
            ItemDorp();
        }
    }

    void UpdateHpUI()
    {
        hpText.text = "HP: " + hp;
    }
    
    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // คำนวณทิศทางของกระสุน
            Vector2 direction = (player.position - firePoint.position).normalized;

            // สร้างกระสุน
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // ตั้งค่าความเร็วของกระสุน
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = direction * 10f;  // เปลี่ยนความเร็วของกระสุนตามที่ต้องการ
            }
        }
    }
    
    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    
    private bool IsObstacleInWay(Vector2 targetPosition)
    {
        // ใช้ Physics2D เพื่อเช็คว่ามี Collider ใน Layer ที่กำหนดหรือไม่
        Collider2D obstacle = Physics2D.OverlapCircle(targetPosition, 0.1f, obstacleLayer);
        return obstacle != null;
    }

    private void ItemDorp()
    {
        if (itemDrops.Length > 0)
        {
            int randomIndex = Random.Range(0, itemDrops.Length);
            Debug.Log("Dropping item: " + itemDrops[randomIndex].name);
            Instantiate(itemDrops[randomIndex], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
        else
        {
            Debug.Log("No items in itemDrops array.");
        }
    }
    
}
