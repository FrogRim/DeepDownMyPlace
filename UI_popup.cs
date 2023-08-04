using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
팝업 UI에 들어갈 컴포넌트
*/
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
        _UIpop();
    }

    public void _UIpop
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
}
