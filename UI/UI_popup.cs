using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_popup : MonoBehaviour
{
    //UI 오프젝트
    public GameObject popupObject;

    private bool Tabstate = false;

    //팝업 키. 기본설정 tab
    public KeyCode pop = KeyCode.Tab;
    //닫는 키. 기본설정 esc
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

    //UI 활성화
    public void _UIpop()
    {
        if (Tabstate)
        {
            popupObject.SetActive(true);
        }
    }
}
