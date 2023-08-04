using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // EventSystem ����ϱ� ����

public abstract class BaseScene : MonoBehaviour // ��� Scene Script�� �ֻ��� �θ�
{
    //Define.Scene _sceneType = Define.Scene.Unknown; // Scene �ʱⰪ Unknown���� ����
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown; // get�� public����, set�� �ڽ�Ŭ������ ���������ϰ� protected�� ������

    // �̷��� �ۼ��ϸ� �ڽ� Ŭ�������� void Start()�� void Awake()������ Init();�� �ۼ����� �ʾƵ� �� �۵���
    void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem)); // EventSystem Component�� ����ִ� Object�� �ִ��� �˻�
        if (obj == null) // �׷� Object�� ���ٸ�
        {
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem"; // EventSystem Prefab�� @EventSystem��� �̸��� Objcet�� ������
        }
    }

    public abstract void Clear(); // Scene�� ����� �� ����/������ �κ� ����
}