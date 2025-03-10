using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerplayer : MonoBehaviour
{
    Rigidbody2D  rb2D;

    Vector2 Move;
    
    float MoveSpeed;
    
   [SerializeField] float sprintSpeed = 0f; // ความเร็วเมื่อกด Shift

    
    private bool isTurningLeft = false;
    //private bool isTurningRight = false;
    
    public LayerMask obstacleLayer;
    
    //private AudioManager audioManager;
    // Start is called before the first frame update

    Animator myAnimation;
    
    /*public SpriteRenderer spriteRenderer; // ใช้สำหรับเปลี่ยน sprite
    public Sprite originalSprite; // รูปปกติของ Player
    public Sprite hitSprite; // รูปที่จะแสดงเมื่อโดนชน*/
    
    void Start()
    {
         rb2D = GetComponent<Rigidbody2D>();
         MoveSpeed = 20f;

         myAnimation = GetComponent<Animator>();
         
         
         // ตรวจสอบว่า spriteRenderer ถูกตั้งค่าหรือไม่
         /*if (spriteRenderer == null)
         {
             spriteRenderer = GetComponent<SpriteRenderer>();
         }

         // เก็บ sprite เดิมไว้เป็น originalSprite
         if (originalSprite == null)
         {
             originalSprite = spriteRenderer.sprite;
         }*/
    }

    // Update is called once per frame
    void Update()
    {
        Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        //animator.SetFloat("Left", false);
        //transform.Translate(Move * MoveSpeed * Time.deltaTime);
        
        Vector2 targetPosition = rb2D.position + Move * MoveSpeed * Time.deltaTime;
        
            // ตรวจสอบว่าผู้เล่นกดปุ่ม Shift หรือไม่
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            MoveSpeed = sprintSpeed; // เพิ่มความเร็วเมื่อกด Shift
        }
        else
        {
            MoveSpeed = 20f; // กลับไปใช้ความเร็วปกติเมื่อปล่อย Shift
        }

        /*if (MoveSpeed == 0)
        {
            myAnimation.SetBool("Walk", false);
        }
        else
        {
            myAnimation.SetBool("Walk", true);
        }*/
        
        // ตรวจสอบว่ามี Obstacle อยู่ข้างหน้า
        if (!IsObstacleInWay(targetPosition))
        {
            rb2D.MovePosition(targetPosition);
        }
        
        filp();
        
        // การเปลี่ยนอาวุธด้วยสกอลเมาส์
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            ChangeWeapon(scroll);
        }
        
        
        //Animation Player
        //Attack();
    }

    void filp()
    {
        if (Move.x < -0.01f)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
            //transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Move.x > -0.01f)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            //transform.localScale = new Vector3(1, 1, 1);
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
    
    private bool IsObstacleInWay(Vector2 targetPosition)
    {
        // ใช้ Physics2D เพื่อเช็คว่ามี Collider ใน Layer ที่กำหนดหรือไม่
        Collider2D obstacle = Physics2D.OverlapCircle(targetPosition, 0.1f, obstacleLayer);
        return obstacle != null;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่าชนกับ tag "EnenmyBullet" หรือไม่
        if (other.CompareTag("EnenmyBullet"))
        {
            // เริ่ม Coroutine เปลี่ยน sprite
            //StartCoroutine(ChangeSpriteTemporarily());
        }
    }
    /*IEnumerator ChangeSpriteTemporarily()
    {
        // เปลี่ยน sprite เป็น hitSprite
        spriteRenderer.sprite = hitSprite;

        // รอเวลา 1 วินาที
        yield return new WaitForSeconds(1f);

        // เปลี่ยน sprite กลับเป็น originalSprite
        spriteRenderer.sprite = originalSprite;
    }*/
    
    
}
