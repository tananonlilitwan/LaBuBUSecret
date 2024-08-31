using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enamyPrefeb;

    [SerializeField] private float minimumSpawnTime;

    [SerializeField] private float maximumSpawnTime;

    private float timeUntilSpawn;

    [SerializeField] Transform[] spawnPoints;

    private void Awake()
    {
        SetTimeUntilSpawn();
        //SpawnEnemy();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            Instantiate(enamyPrefeb, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
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
    
}