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
    
}
