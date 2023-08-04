using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel // UI_Inven Object의 자식
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

        /* 반복문을 돌면서 GridPanel이 가진 자식을 다 삭제하는 코드
        foreach (Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }*/

        
        for (int i = 0; i < 3; i++)
        {
           
            // MakeSubItem을 사용하면 위코드를 줄일 수 있음
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;

          

            item.GetComponent<UI_Inven_Item>().SetInfo($"{i+1}번 무기");
            item.GetComponent<UI_Inven_Item>().itemnum = i + 1;
        }
    }
}

