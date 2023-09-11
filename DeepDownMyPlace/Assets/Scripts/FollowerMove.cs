using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMove : MonoBehaviour
{
    private float moveSpeed = 5f; // �̵� �ӵ� ���� ����
    private Rigidbody2D rb;
    public Define.Character Roll;
    void Start()
    {
      Roll = Define.Character.Follower;
      rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rb.velocity = Vector2.left * moveSpeed;
    }
}
