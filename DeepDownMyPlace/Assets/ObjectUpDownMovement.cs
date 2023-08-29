using UnityEngine;

public class ObjectUpDownMovement : MonoBehaviour
{
    public float movementDistance = 1.0f; // 이동 거리
    public float movementSpeed = 1.0f;    // 이동 속도

    
    private bool movingUp = true; // 위로 움직이는 중인지 여부

    private void Start()
    {
        
    }

    private void Update()
    {
        // 오브젝트의 현재 Y 위치
        float currentY = transform.position.y;

        // 위아래 이동 왕복
        if (movingUp)
        {
            currentY += movementSpeed * Time.deltaTime;
        }
        else
        {
            currentY -= movementSpeed * Time.deltaTime;
        }

        // 이동 범위를 벗어났을 때 방향 전환
        if (currentY >=  movementDistance)
        {
            movingUp = false;
        }
        else if (currentY <= movementDistance)
        {
            movingUp = true;
        }

        // 새로운 Y 위치 적용
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}
