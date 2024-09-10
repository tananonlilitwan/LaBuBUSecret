using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DorpItemHp : MonoBehaviour
{
    [SerializeField] private GameObject HpPrefab;

    [SerializeField] private float spawnInterval = 2f;  // ความถี่ในการดรอปไอเท็มทุก ๆ 10 วินาที

    private float timeUntilSpawn;

    [SerializeField] private Transform[] spawnPoints;  // จุดที่สามารถดรอปไอเท็มได้
    
    
    private void Awake()
    {
        timeUntilSpawn = spawnInterval;  // ตั้งเวลารอในการดรอปครั้งแรก
    }

    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            for (int i = 0; i < 1; i++)  // ทำการดรอปไอเท็ม 1 ชิ้น
            {
                SpawnItem();  // เรียกใช้ฟังก์ชันในการดรอปไอเท็ม
                
            }
            timeUntilSpawn = spawnInterval;  // รีเซ็ตเวลารอในการดรอปครั้งถัดไป
        }
    }

    private void SpawnItem()
    {
        if (spawnPoints.Length > 0)
        {
            // เลือกจุดเกิดแบบสุ่มจาก spawnPoints array
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
            // สร้างไอเท็มที่ตำแหน่งที่เลือก
            GameObject spawnedItem = Instantiate(HpPrefab, spawnPoint.position, Quaternion.identity);
            
            // เพิ่มคอมโพเนนต์ Collider2D และ Rigidbody2D ให้กับไอเท็มเพื่อให้มันสามารถโต้ตอบกับผู้เล่นได้
            if (spawnedItem.GetComponent<Collider2D>() == null)
            {
                spawnedItem.AddComponent<BoxCollider2D>().isTrigger = true;
            }
            if (spawnedItem.GetComponent<Rigidbody2D>() == null)
            {
                spawnedItem.AddComponent<Rigidbody2D>().gravityScale = 0;
            }
        
            // Debug log สำหรับตรวจสอบตำแหน่งที่ไอเท็มถูกสร้าง
            Debug.Log("Spawned item at " + spawnPoint.position);
        }
        else
        {
            Debug.LogError("No spawn points assigned.");
        }
    }
    
}