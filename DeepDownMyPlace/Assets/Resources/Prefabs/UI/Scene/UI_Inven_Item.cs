using System.Collections;
using System.Collections.Generic;
using TMPro; // TMP_Text 사용하기 위함
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    public int itemnum;
   
    

    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
        
    }

    string _name;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TMP_Text>().text = _name; // text를 _name으로 변경

        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"아이템 클릭! {_name}"); }); // 아이콘 클릭하면 로그 찍기

        
    }

    public void SetInfo(string name) // 외부에서 이름을 세팅하는 함수
    {
        _name = name; // 받아온 이름을 _name에 저장
    }

    private void Update()
    {
        
    }
}