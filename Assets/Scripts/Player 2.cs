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
        // Right Shift Key -> �����̰� ���� �� �� �������� ȸ��
        dDown = Input.GetButtonDown("Dodge2");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        // ȸ�� ���� ������ �ٲ��� �ʰ� ��
        if (isDodge)
        {
            moveVec = dodgeVec;
        }

        transform.position += moveVec * speed * Time.deltaTime;
        anim.SetBool("isMove", moveVec != Vector3.zero);
    }

    // �����̴� �������� �ٶ�
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

            // 0.5�� �Ŀ� ȸ�� ����
            Invoke("DodgeOut", 0.5f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }
}
