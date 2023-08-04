using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_popup : MonoBehaviour
{
    //UI ������Ʈ
    public GameObject popupObject;

    private bool Tabstate = false;

    //�˾� Ű. �⺻���� tab
    public KeyCode pop = KeyCode.Tab;
    //�ݴ� Ű. �⺻���� esc
    public KeyCode close = KeyCode.Escape;

    void Update()
    {
        isTabKeyPressed();
        _UIpop();
    }

    public void isTabKeyPressed()
    {
        if (Input.GetKey(pop))
        {
            popupObject.SetActive(true);
        }
        else if(Input.GetKeyUp(pop) || Input.GetKey(close))
        {
            popupObject.SetActive(false);
        }       
    }

    //UI Ȱ��ȭ
    public void _UIpop()
    {
        if (Tabstate)
        {
            popupObject.SetActive(true);
        }
    }
}
