using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enamyPrefeb;
    
    [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private GameObject enemyPrefab3;
    [SerializeField] private GameObject enemyPrefab4;

    [SerializeField] private float minimumSpawnTime;

    [SerializeField] private float maximumSpawnTime;

    private float timeUntilSpawn;

    [SerializeField] Transform[] spawnPoints;
    
    //private bool isSpawning = true; // ใช้เพื่อควบคุมการปล่อยศัตรู
    private bool isSpawning = false;
    
    
    
    private void Awake()
    {
        //SetTimeUntilSpawn();
        //SpawnEnemy();
        StartCoroutine(SpawnAndPauseCycle());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        /*if (timeUntilSpawn <= 0)
        {
            Instantiate(enamyPrefeb, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }*/
        
        
        if (isSpawning)
        {
            timeUntilSpawn -= Time.deltaTime;

            if (timeUntilSpawn <= 0)
            {
                //SpawnEnemy();
                //SetTimeUntilSpawn();
                StartCoroutine(SpawnAndPauseCycle());
            }
        }
        
    }
    
    
    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
    
    private void SpawnEnemy()
    {
        // เลือกจุดเกิดแบบสุ่มจาก spawnPoints array
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        // สร้างศัตรูที่ตำแหน่งที่เลือก
        Instantiate(enamyPrefeb, spawnPoint.position, Quaternion.identity);
        
        // Debug log สำหรับตรวจสอบตำแหน่งที่ศัตรูถูกสร้าง
        Debug.Log("Spawned enemy at " + spawnPoint.position);
    }
    
    
    private IEnumerator SpawnAndPauseCycle()
    {
        // Prefabs สำหรับแต่ละ Wave
        GameObject[] enemyPrefabs = { enamyPrefeb, enemyPrefab1, enemyPrefab2, enemyPrefab3, enemyPrefab4 };

        // จำนวนศัตรูที่ต้องปล่อยในแต่ละ Wave
        int[] waveEnemyCounts = { 20, 25, 30, 40, 60 }; // ทุก Wave มี 10 ตัว

        // ระยะเวลาพักหลังแต่ละ Wave
        float[] wavePauses = { 2f, 3f, 5f, 10f, 10f }; // แต่ละ Wave พัก 10 วินาที

        for (int wave = 0; wave < waveEnemyCounts.Length; wave++)
        {
            Debug.Log($"Starting wave {wave + 1} with {waveEnemyCounts[wave]} enemies.");

            // ปล่อยศัตรูจาก Prefab ตามจำนวนในแต่ละ Wave
            yield return StartCoroutine(SpawnEnemies(enemyPrefabs[wave], waveEnemyCounts[wave]));

            Debug.Log($"Completed wave {wave + 1}. Waiting for {wavePauses[wave]} seconds.");

            // พักตามเวลาที่กำหนดในแต่ละ Wave
            yield return new WaitForSeconds(wavePauses[wave]);
        }

        // เมื่อปล่อยครบทุก Wave แล้ว หยุดการ Spawn
        isSpawning = false;
        Debug.Log("All waves completed.");
    } 
    
    private IEnumerator SpawnEnemies(GameObject enemyPrefab, int totalCount)
    {
        float spawnInterval = 1f; // เวลาระหว่างการปล่อยกลุ่มศัตรู
        int batchSize = 3; // จำนวนศัตรูที่ปล่อยในแต่ละช่วงเวลา

        while (totalCount > 0)
        {
            // คำนวณจำนวนศัตรูที่ปล่อยในรอบนี้ (ไม่เกินจำนวนที่เหลือ)
            int currentBatchSize = Mathf.Min(batchSize, totalCount);

            for (int i = 0; i < currentBatchSize; i++)
            {
                Vector3 spawnPoint = GetRandomSpawnPoint();
                Debug.Log($"Spawning enemy {totalCount - i} at {spawnPoint}");
                Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            }

            totalCount -= currentBatchSize; // ลดจำนวนศัตรูที่เหลือ

            // รอเวลาตามที่กำหนดก่อนปล่อยกลุ่มศัตรูถัดไป
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    
    
    /*private IEnumerator SpawnEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Debug.Log($"Spawning enemy {i + 1} at {spawnPoint}");
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            Debug.Log($"Enemy instantiated: {enemy.name} at {spawnPoint}");
            yield return new WaitForSeconds(1f); // เว้น 1 วินาทีระหว่างการปล่อยแต่ละตัว
        }
    }*/

    // ฟังก์ชันสำหรับหาตำแหน่งสุ่มในการ spawn
    private Vector3 GetRandomSpawnPoint()
    {
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 position = randomSpawnPoint.position;
        Debug.Log($"Selected spawn point: {position}");
        return position;
    }
    
    
    
    
    
    
    // code ไม่ได้ใช้
    
    /*private IEnumerator SpawnAndPauseCycle()
    {
        while (true)
        {
            // ปล่อยศัตรูเป็นเวลา 5 วินาที
            isSpawning = true;
            yield return new WaitForSeconds(5f); // <- แก้ตัวเลขตรงนี้

            // หยุดปล่อยศัตรูเป็นเวลา 10 วินาที
            isSpawning = false;
            yield return new WaitForSeconds(10f); // <- แก้ตัวเลขตรงนี้
        }
    }*/
    
    
    
    /*private IEnumerator SpawnEnemies(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // เลือกจุดเกิดแบบสุ่มจาก spawnPoints array
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // สร้างศัตรูที่ตำแหน่งที่เลือก
            Instantiate(enamyPrefeb, spawnPoint.position, Quaternion.identity);

            // เพิ่มการหน่วงเวลาเล็กน้อยระหว่างการปล่อยแต่ละศัตรู (ถ้าต้องการ)
            yield return new WaitForSeconds(0.5f);  // ปล่อยศัตรูทุกๆ 0.5 วินาที
        }
    }*/
    
    // ฟังก์ชันสำหรับการปล่อยศัตรู
    /*private IEnumerator SpawnEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            yield return new WaitForSeconds(1f); // เว้น 1 วินาทีระหว่างการปล่อยแต่ละตัว
        }
    }*/
    
    
}