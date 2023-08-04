using System.Collections;
using System.Collections.Generic;
using TMPro; // TMP_Text ����ϱ� ����
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    public int itemnum;
    weapon weapon;
    

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
        weapon = GameObject.Find("MC01").GetComponent<weapon>();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TMP_Text>().text = _name; // text�� _name���� ����

        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"������ Ŭ��! {_name}"); }); // ������ Ŭ���ϸ� �α� ���

        
    }

    public void SetInfo(string name) // �ܺο��� �̸��� �����ϴ� �Լ�
    {
        _name = name; // �޾ƿ� �̸��� _name�� ����
    }

    private void Update()
    {
        if (weapon.weaponIndex == itemnum - 1)
        {
            Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
        else
        {
            Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
}