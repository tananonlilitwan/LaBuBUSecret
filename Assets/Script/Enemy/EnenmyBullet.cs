using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class EnenmyBullet : MonoBehaviour
{

    private GameObject player;
    private Rigidbody2D rd;
    public float force;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rd.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
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
