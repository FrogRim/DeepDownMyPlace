using System.Collections;
using System.Collections.Generic;
using TMPro; // TMP_Text 사용하기 위함
using UnityEngine;
using UnityEngine.EventSystems; // PointerEventData 사용하기 위함
using UnityEngine.UI; // Text 사용하기 위함

public class UI_Button : UI_Popup // 간접적으로 MonoBehaviour을 상속받고(UI_Base가 상속받고 있음) 있기 때문에 Object의 Script Component로 사용 가능
{
   
    

    enum Buttons // Button Object 목록 작성
    {
        PointButton
    }

    enum Texts // Text Object 목록 작성
    {
        PointText,
        ScoreText
    }

    enum Images // Image Object 목록 작성
    {
        ItemIcon
    }

    enum GameObjects // UI와 관련없는 Object 목록 작성
    {
        TestObject,
        //ScoreText
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Bind<Button>(typeof(Buttons)); // Button Component(<Button>)를 찾아 해당하는 Object를 매핑해라
        //Bind<Text>(typeof(Texts)); // Text Component(<Text>)를 찾아 해당하는 Object를 매핑해라
        Bind<TMP_Text>(typeof(Texts)); // Text Component(<TMP_Text>)를 찾아 해당하는 Object를 매핑해라
        //Debug.Log($"{typeof(Buttons)}"); // UI_Button+Buttons
        //Debug.Log($"{typeof(Texts)}"); // UI_Button+Texts
        Bind<Image>(typeof(Images)); // Image Component(<Image>)를 찾아 해당하는 Object를 매핑해라
        Bind<GameObject>(typeof(GameObjects)); // GameObject Component(<GameObject>)를 찾아 해당하는 Object를 매핑하라는 의미지만, GameObject는 component가 아니라 오류 발생 // FindChild 함수 수정 필요

        //Get<Text>((int)Texts.ScoreText).text = "Bind Test"; // 함수 테스트용
        //Get<TMP_Text>((int)Texts.ScoreText).text = "Bind Test"; // 함수 테스트용
        //GetText((int)Texts.ScoreText).text = "Bind Test"; // 함수 테스트용

          GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked); // Extension Method를 사용해서 Button 클릭 이벤트 구현


        // UI_EventHandler에서 바로 이벤트를 호출하지 않고, 구독형식으로 처리함
        GameObject go = GetImage((int)Images.ItemIcon).gameObject; // Image Component를 가진 ItemIcon Object를 GameObject로 변환
        UI_EventHandler evt = go.GetComponent<UI_EventHandler>(); // Object로부터 UI_EventHandler Component를 찾음
        evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; }); // 함수를 선언하며 구독신청 // evt.gameObject로 ItemIcon Object를 찾고 transform component로 내려감

        // 바로 위코드 간편화
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

        // #참고 // 이해가 안된다면 이 부분은 넘어가기
        //Get<GameObject>((int)GameObjects.ScoreText).transform.GetComponent<TMP_Text>().text = "Bind Test2"; // 따지고 보면 ScoreText도 GameObject, GameObject로 ScoreText를 찾고, 거기서 TMP_Text Component를 찾아서, 텍스트 변경 가능
    }

    int _score = 0;

    public void OnButtonClicked(PointerEventData data) 
    {
        Debug.Log("ButtonClicked!");

        _score++;

        GetText((int)Texts.ScoreText).text = $"점수 : {_score}점";
    }


}