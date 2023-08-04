using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel // UI_Inven Object�� �ڽ�
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

        /* �ݺ����� ���鼭 GridPanel�� ���� �ڽ��� �� �����ϴ� �ڵ�
        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }*/

        
        for (int i = 0; i < 3; i++)
        {
           
            // MakeSubItem�� ����ϸ� ���ڵ带 ���� �� ����
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;

          

            item.GetComponent<UI_Inven_Item>().SetInfo($"{i+1}�� ����");
            item.GetComponent<UI_Inven_Item>().itemnum = i + 1;
        }
    }
}

