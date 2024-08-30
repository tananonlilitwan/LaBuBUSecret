using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerplayer : MonoBehaviour
{
    Rigidbody2D  rb2D;

    Vector2 Move;
    
    float MoveSpeed;
    
    
    public Animator animator;
    private bool isTurningLeft = false;
    //private bool isTurningRight = false;
    
    public LayerMask obstacleLayer;
    
    //private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
         rb2D = GetComponent<Rigidbody2D>();
         MoveSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        //animator.SetFloat("Left", false);
        transform.Translate(Move * MoveSpeed * Time.deltaTime);
        
        filp();
        
        // การเปลี่ยนอาวุธด้วยสกอลเมาส์
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            ChangeWeapon(scroll);
        }
        
        if (!IsObstacleInWay(transform.position))
        {
            rb2D.MovePosition(transform.position);
        }

    }

    void filp()
    {
        if (Move.x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Move.x > -0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    
    void ChangeWeapon(float scroll)
    {
        if (scroll > 0)
        {
            Debug.Log("เปลี่ยนอาวุธไปด้านหน้า");
        }
        else if (scroll < 0)
        {
            Debug.Log("เปลี่ยนอาวุธไปด้านหลัง");
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            // ถ้าชนกับ Tag "Untagged", ป้องกันไม่ให้เคลื่อนที่
            rb2D.velocity = Vector2.zero; // หยุดการเคลื่อนที่
        }
    }
    
    private bool IsObstacleInWay(Vector2 targetPosition)
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D obstacle = Physics2D.OverlapCircle(targetPosition, playerCollider.bounds.extents.x, obstacleLayer);
        return obstacle != null;
    }
    
}
