using System; // Action 사용하기 위함
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // EventSystem 사용하기 위함

public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    public void OnUpdate()
    {
        // UI를 클릭해도 계속 이벤트를 발생시키기 때문에 캐릭터가 이동하는 상황을 방지하기 위해 추가
        if (EventSystem.current.IsPointerOverGameObject()) // UI Object가 클릭되었다면
        {
            return; // 그냥 return
        }

        if (Input.anyKey && KeyAction != null)
        {
            KeyAction.Invoke();
        }

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.Click);
                }
                _pressed = false;
            }
        }
    }

    public void Clear() // Scene이 바뀔때 초기화
    {
        KeyAction = null;
        MouseAction = null;
    }
}