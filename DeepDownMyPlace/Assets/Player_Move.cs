using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도 조절 변수
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 입력을 받아 이동
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // normalized를 사용하여 대각선 이동 속도가 너무 빨라지는 것을 방지
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // 캐릭터를 이동 방향으로 회전
        if (moveDirection != Vector2.zero)
        {
            // 좌우 이동 시에만 회전
            if (Mathf.Abs(horizontalInput) > 0f)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, (horizontalInput > 0f) ? 180f : 0f, 0f);
            }
        }

        // Rigidbody를 사용하여 이동
        rb.velocity = moveDirection * moveSpeed;
    }

}
