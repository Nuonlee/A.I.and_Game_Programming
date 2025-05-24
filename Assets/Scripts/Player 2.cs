using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool dDown;

    bool isDodge;

    Vector3 moveVec;
    Vector3 dodgeVec;

    Rigidbody rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Dodge();
    }

    // ------------------------------------------------ Update()
    void GetInput()
    {
        // Arrow Key
        hAxis = Input.GetAxisRaw("Horizontal2");
        vAxis = Input.GetAxisRaw("Vertical2");
        // Right Shift Key -> 움직이고 있을 때 그 방향으로 회피
        dDown = Input.GetButtonDown("Dodge2");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        // 회피 도중 방향을 바꾸지 않게 함
        if (isDodge)
        {
            moveVec = dodgeVec;
        }

        transform.position += moveVec * speed * Time.deltaTime;
        anim.SetBool("isMove", moveVec != Vector3.zero);
    }

    // 움직이는 방향으로 바라봄
    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Dodge()
    {
        if (dDown && !isDodge && moveVec != Vector3.zero)
        {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            // 0.5초 후에 회피 해제
            Invoke("DodgeOut", 0.5f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }
}
