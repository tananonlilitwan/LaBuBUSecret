using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator myAnimation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myAnimation.SetBool("Attack", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            myAnimation.SetBool("Attack", false);
        }
    }
}
