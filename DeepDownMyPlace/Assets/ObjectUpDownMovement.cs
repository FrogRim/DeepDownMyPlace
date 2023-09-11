using UnityEngine;

public class ObjectUpDownMovement : MonoBehaviour
{
    public float movementDistance = 1.0f; // �̵� �Ÿ�
    public float movementSpeed = 1.0f;    // �̵� �ӵ�

    
    private bool movingUp = true; // ���� �����̴� ������ ����

    private void Start()
    {
        
    }

    private void Update()
    {
        // ������Ʈ�� ���� Y ��ġ
        float currentY = transform.position.y;

        // ���Ʒ� �̵� �պ�
        if (movingUp)
        {
            currentY += movementSpeed * Time.deltaTime;
        }
        else
        {
            currentY -= movementSpeed * Time.deltaTime;
        }

        // �̵� ������ ����� �� ���� ��ȯ
        if (currentY >=  movementDistance)
        {
            movingUp = false;
        }
        else if (currentY <= movementDistance)
        {
            movingUp = true;
        }

        // ���ο� Y ��ġ ����
        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}
