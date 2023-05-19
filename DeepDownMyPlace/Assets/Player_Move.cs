using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    public Sprite[] imgs;
    public SpriteRenderer current;
    

    Rigidbody2D player_rb;
    Animator animator;
    public float _speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        float moveX = 0f;
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            player_rb.velocity = (transform.right * -1) * _speed;
        }


        if (Input.GetKey(KeyCode.Alpha1)) current.sprite = imgs[0];
        if (Input.GetKey(KeyCode.Alpha2)) current.sprite = imgs[1];
        if (Input.GetKey(KeyCode.Alpha3)) current.sprite = imgs[2];


    }
}
