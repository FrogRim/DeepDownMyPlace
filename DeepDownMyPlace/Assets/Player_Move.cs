using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Move : MonoBehaviour
{
    // 플레이어의 이동 속도를 조절하는 변수
    public float speed = 5f;

    
    private Rigidbody2D rb;
    private Vector2 target;

   
    void Start()
    {
        // 플레이어의 Rigidbody2D 컴포넌트를 가져와서 rb 변수에 저장합니다.
        rb = GetComponent<Rigidbody2D>();

        // 처음에는 플레이어의 현재 위치를 target 변수에 저장합니다.
        target = transform.position;
    }

    
    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭하면
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스의 현재 위치를 화면 좌표계에서 월드 좌표계로 변환하고 target 변수에 저장합니다.
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            
        }
    }

    // FixedUpdate 함수는 고정된 시간 간격마다 실행됩니다. 물리적 연산은 여기서 처리해야 합니다.
    void FixedUpdate()
    {
        // 플레이어의 현재 위치와 target 위치 사이의 거리를 계산합니다.
        float distance = Vector2.Distance(transform.position, target);

        // 거리가 0.1f보다 크면 (아직 도착하지 않았으면)
        if (distance > 0.1f)
        {
            // 플레이어의 현재 위치와 target 위치 사이의 방향 벡터를 구합니다.
            Vector2 direction = (target - (Vector2)transform.position).normalized;

            // 방향 벡터에 속도를 곱하여 이동 벡터를 구합니다.
            Vector2 movement = direction * speed * Time.fixedDeltaTime;

            // Rigidbody2D 컴포넌트의 MovePosition 함수를 사용하여 플레이어를 이동시킵니다.
            rb.MovePosition((Vector2)transform.position + movement);
        }
    }
}

