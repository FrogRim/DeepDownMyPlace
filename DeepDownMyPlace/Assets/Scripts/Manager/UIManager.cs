using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10; // 최근에 사용한 order값 저장 // scene의 order값이 0, sort해줄때의 시작값이 0이면 scene과의 차별점이 없음, 그래서 처음 시작값을 10(0이 아닌수)으로 시작하기 // 0~9는 UI를 먼저 띄우고싶으면 그 때 사용하면 됨

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); // GameObject를 들고 있어도 되지만, 팝업이 가지고 있는 각 Script component가 UI_Popup을 다들 상속받고 있기 때문에, UI_Popup component를 들고 있는 것이 나음
    UI_Scene _sceneUI = null; // 일반 UI는 _sceneUI에 저장

    // UI들을 하나의 폴더 안에 띄우고 싶어서 폴더를 만들어주는 방법 // 물론 실제로는 폴더는 아님(빈 오브젝트를 폴더 처럼 사용)
    public GameObject Root // 프로퍼티로 생성
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root"); // 해당 Object를 찾고
            if (root == null) // Object가 없으면
            {
                root = new GameObject { name = "@UI_Root" }; // 새로 만들어주기
            }
            return root;
        }
    }
    public void SetCanvas(GameObject go, bool sort = true) // 외부에서 팝업을 띄울 때, 그 팝업 Canvas의 Sort Order을 채워주는 함수 // (Object, sort 여부)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go); // Canvas Component 추출
        canvas.renderMode = RenderMode.ScreenSpaceOverlay; // Render Mode는 ScreenSpaceOverlay로 해줘야함
        canvas.overrideSorting = true; // 현재 Canvas가 다른 Canvas 안에 중첩해서 있을때, 부모가 어떤 Sort Order 값을 가지든 상관없이, 현재 Canvas만의 Sort Order값을 가질 것이란 의미

        if (sort) // sort 요청이 있었다면(true)
        {
            canvas.sortingOrder = _order; // sortingOrder을 _order값으로 셋팅 후
            _order++; // 1 증가
        }
        else // sort 요청이 없었다면, 즉, Popup이랑 연관없는 일반 UI라면
        {
            canvas.sortingOrder = 0; // 0으로 세팅
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene // 일반 UI 띄우기 // name은 Prefab의 이름, T는 Script(component)의 이름 // T는 UI_Scene을 상속받는 Script들
    {
        if (string.IsNullOrEmpty(name)) // name을 안받았다면
        {
            name = typeof(T).Name; // T의 이름으로 name에 저장
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}"); // Prefab에서 꺼내와 만든 Object를 go에 저장
        T sceneUI = Util.GetOrAddComponent<T>(go); // T Script component를 찾아와 T타입의 popup에 저장
        _sceneUI = sceneUI;
        //_order++; // ShowPopupUI를 통해 Popup을 띄운 것이 아니라, 시작할 때부터 Scene에 만들어 놓은 경우에 문제 발생

        go.transform.SetParent(Root.transform); // go의 부모는 root로 지정

        return sceneUI; // T타입의 popup을 return
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup // 팝업 띄우기 // name은 Prefab의 이름, T는 Script(component)의 이름 // T는 UI_Popup을 상속받는 Script들
    {
        if (string.IsNullOrEmpty(name)) // name을 안받았다면
        {
            name = typeof(T).Name; // T의 이름으로 name에 저장
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}"); // Prefab에서 꺼내와 만든 Object를 go에 저장
        T popup = Util.GetOrAddComponent<T>(go); // T Script component를 찾아와 T타입의 popup에 저장
        _popupStack.Push(popup); // popup을 _popupStack에 저장
        //_order++; // ShowPopupUI를 통해 Popup을 띄운 것이 아니라, 시작할 때부터 Scene에 만들어 놓은 경우에 문제 발생

        go.transform.SetParent(Root.transform); // go의 부모는 root로 지정

        return popup; // T타입의 popup을 return
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base // SubItem 만들기 // parent는 SubItem이 들어갈 부모의 transform, name은 Prefab의 이름, T는 Script(component)의 이름 // T는 UI_Base를 상속받는 Script들
    {
        if (string.IsNullOrEmpty(name)) // name을 안받았다면
        {
            name = typeof(T).Name; // T의 이름으로 name에 저장
        }

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}"); // Prefab에서 꺼내와 만든 Object를 go에 저장
        if (parent != null) // SubItem이 들어갈 부모의 transform 정보가 주어진다면
        {
            go.transform.SetParent(parent); // go의 부모를 parent로 설정
        }

        return Util.GetOrAddComponent<T>(go); // T Script를 component로 추가하며 T타입 반환
    }

    public void ClosePopupUI(UI_Popup popup) // 닫을 팝업을 명시해줘서, 닫힐 팝업이 popup이 맞는지 확인 // 좀 더 안정적인 버전
    {
        if (_popupStack.Count == 0) // _popupStack에 아무것도 없다면
        {
            return; // 그냥 return
        }

        if (_popupStack.Peek() != popup) // Peek로 마지막 Popup을 엿보고와서, popup과 비교했는데 다르면
        {
            Debug.Log("Close Popup Failed!");
            return; // 그냥 return
        }

        ClosePopupUI(); // 여기까지 문제없었으면 팝업 그냥 닫기
    }

    public void ClosePopupUI() // 팝업 닫기
    {
        if (_popupStack.Count == 0) // _popupStack에 아무것도 없다면
        {
            return; // 그냥 return
        }

        UI_Popup popup = _popupStack.Pop(); // 가장 최근에 띄운 popup을 가져와 UI_Popup타입의 popup에 저장
        Managers.Resource.Destroy(popup.gameObject); // popup으로 Object를 찾아 삭제
        popup = null; // popup은 삭제됐으니 접근하면 안됨, 혹시 몰라 null로 초기화
        _order--;
    }

    public void CloseAllPopupUI() // 모든 팝업을 다 지우기
    {
        while (_popupStack.Count > 0) // _popupStack에 아무것도 없을 때까지
        {
            ClosePopupUI();
        }
    }

    public void Clear()  // Scene이 바뀔때 초기화
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}