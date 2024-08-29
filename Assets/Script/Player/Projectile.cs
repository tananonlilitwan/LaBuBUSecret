using System;
using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbbulletprefab;
    [SerializeField] private GameObject PosShool; // อะไรที่เป็นOBJ ในเกม ให้ใช้ ตัวแปรชนิด GameObject
    [SerializeField] private Transform ShootPoint;
   
    private float timer;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        timer += Time.deltaTime;
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (timer > 5)
            {
                Destroy(gameObject);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            //                                      ให้ ray ไปดีเทคที่ไหน 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            // สร้างแสง rat                            ความยาวของแสง  สีของ ray     โชวแสง 10วิ
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.magenta, 10f);

            //                            ray ไป cos อะไรก็แล้วแต่      ระยะยิงไม่จำกัด ถ้า rayไป ชน ให้เก็บที่ hiy2d
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit2D.centroid != null)
            {
                //ย้ายไปอยู่ที่เมาล์คลิ๊ก                                       ย้ายไปตำแหน่งใหม่ 
                PosShool.transform.position = new Vector2(hit2D.point.x, hit2D.point.y);
                //แสดงตำแหน่งในunity
                Debug.Log($"hit2D point: {hit2D.point.x}, {hit2D.point.y} ");
                
                // FIRE BULLET in projecttile motion
               Vector2 projectile = CalculateProjecttileVelocity(ShootPoint.transform.position, hit2D.point, 1f);
               Rigidbody2D fireBuller = Instantiate(rbbulletprefab, ShootPoint.transform.position, quaternion.identity);
               fireBuller.velocity = projectile;

            }
            
        }
        

    }

    //ยิง rbbulletprefab ออกไป จาก posShoot
                       //             จุดเริ่มต้น         จุดปลายทาง            t = time เวลา
    Vector2 CalculateProjecttileVelocity(Vector2 origin, Vector2 PosShool, float t )
    {  
        // ระยะทางหระว่าง จุดสองจุด
        Vector2 distance = PosShool - origin;

        float distX = distance.x;
        float disty = distance.y;

        //       ความเร็ว แกน x หาร เวลา
        float velocityX = distX / t;
        //       ความเร็ว แกน Y หาร เวลา บวก 0.5f คุณ ฟิสคิ 2d กาวิตี้ y คูณ เวลา
        float velocityY = disty / t + 0.5f * Mathf.Abs(Physics2D.gravity.y) * t;
        
        // ส่งออกไปผ่านตัวแปร Vector2
        Vector2 result = new Vector2(velocityX, velocityY);
        return result;

    }
    
    
    
}

