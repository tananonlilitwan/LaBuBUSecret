using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = System.Numerics.Quaternion;

public class EnenmyBullet : MonoBehaviour
{

    private GameObject player;
    //private Rigidbody2D rd;
    //public float force;
    [SerializeField] private float speed = 5f; // ความเร็วของกระสุน ที่สามารถแก้ไขใน Unity
    private Vector3 direction;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        // หา GameObject ของผู้เล่น
        player = GameObject.FindGameObjectWithTag("Player");

        // คำนวณทิศทางจากกระสุนไปยังผู้เล่น
        direction = (player.transform.position - transform.position).normalized;
        
        transform.position += direction * speed * Time.deltaTime;
        

        
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        timer += Time.deltaTime;
        if (other.gameObject.CompareTag("Player"))
        {
            if (timer > 5)
            {
                //Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
        if (other.tag == "Player" )
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Untagged"))
        {
            // ลบ BulletPlayer ออก
            Destroy(gameObject);
        }
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnenmyBullet"))
        {
            Destroy(other.gameObject);
        }
    }
    */
}
