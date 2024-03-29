using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // EventSystem 사용하기 위함

public abstract class BaseScene : MonoBehaviour // 모든 Scene Script의 최상위 부모
{
    //Define.Scene _sceneType = Define.Scene.Unknown; // Scene 초기값 Unknown으로 설정
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown; // get은 public으로, set은 자식클래스만 수정가능하게 protected로 열어줌

    // 이렇게 작성하면 자식 클래스에서 void Start()나 void Awake()문에서 Init();을 작성하지 않아도 잘 작동함
    void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem)); // EventSystem Component를 들고있는 Object가 있는지 검색
        if (obj == null) // 그런 Object가 없다면
        {
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem"; // EventSystem Prefab을 @EventSystem라는 이름의 Objcet로 꺼내옴
        }
    }

    public abstract void Clear(); // Scene이 종료될 때 정리/삭제할 부분 정의
}