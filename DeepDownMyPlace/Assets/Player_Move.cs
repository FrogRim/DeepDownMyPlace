using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Move : MonoBehaviour
{
    // �÷��̾��� �̵� �ӵ��� �����ϴ� ����
    public float speed = 5f;

    
    private Rigidbody2D rb;
    private Vector2 target;

   
    void Start()
    {
        // �÷��̾��� Rigidbody2D ������Ʈ�� �����ͼ� rb ������ �����մϴ�.
        rb = GetComponent<Rigidbody2D>();

        // ó������ �÷��̾��� ���� ��ġ�� target ������ �����մϴ�.
        target = transform.position;
    }

    
    void Update()
    {
        // ���콺 ���� ��ư�� Ŭ���ϸ�
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺�� ���� ��ġ�� ȭ�� ��ǥ�迡�� ���� ��ǥ��� ��ȯ�ϰ� target ������ �����մϴ�.
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            
        }
    }

    // FixedUpdate �Լ��� ������ �ð� ���ݸ��� ����˴ϴ�. ������ ������ ���⼭ ó���ؾ� �մϴ�.
    void FixedUpdate()
    {
        // �÷��̾��� ���� ��ġ�� target ��ġ ������ �Ÿ��� ����մϴ�.
        float distance = Vector2.Distance(transform.position, target);

        // �Ÿ��� 0.1f���� ũ�� (���� �������� �ʾ�����)
        if (distance > 0.1f)
        {
            // �÷��̾��� ���� ��ġ�� target ��ġ ������ ���� ���͸� ���մϴ�.
            Vector2 direction = (target - (Vector2)transform.position).normalized;

            // ���� ���Ϳ� �ӵ��� ���Ͽ� �̵� ���͸� ���մϴ�.
            Vector2 movement = direction * speed * Time.fixedDeltaTime;

            // Rigidbody2D ������Ʈ�� MovePosition �Լ��� ����Ͽ� �÷��̾ �̵���ŵ�ϴ�.
            rb.MovePosition((Vector2)transform.position + movement);
        }
    }
}

