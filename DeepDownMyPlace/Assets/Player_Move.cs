using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ� ���� ����
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // �Է��� �޾� �̵�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // normalized�� ����Ͽ� �밢�� �̵� �ӵ��� �ʹ� �������� ���� ����
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // ĳ���͸� �̵� �������� ȸ��
        if (moveDirection != Vector2.zero)
        {
            // �¿� �̵� �ÿ��� ȸ��
            if (Mathf.Abs(horizontalInput) > 0f)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, (horizontalInput > 0f) ? 180f : 0f, 0f);
            }
        }

        // Rigidbody�� ����Ͽ� �̵�
        rb.velocity = moveDirection * moveSpeed;
    }

}
