using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ---------------------------
    // 싱글톤 패턴 구축
    //----------------------------
    static GameManager Instance; // static 변수 선언을 통해 유일성 보장
    public static GameManager GetInstance { get{ return Instance; } } // GetInstance 프로퍼티를 통해 다른 스크립트에서 Instance를 가져 올 수 있다.
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Init() // 
    {
        if (Instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            if (go == null)
            {
                go = new GameObject { name = "GameManager" };
                go.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(go);
            Instance = go.GetComponent<GameManager>();
        }
        
    }


}
