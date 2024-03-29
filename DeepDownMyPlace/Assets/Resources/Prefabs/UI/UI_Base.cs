using System; // Type 사용하기 위함
using System.Collections;
using System.Collections.Generic;
using TMPro; // TMP_Text 사용하기 위함
using UnityEngine;
using UnityEngine.EventSystems; // PointerEventData 사용하기 위함
using UnityEngine.UI; // Text 사용하기 위함

public abstract class UI_Base : MonoBehaviour // UI Canvas에 붙는 Script Component들의 부모
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>(); // Button Type은 Button의 목록들을 들고 있고, Text Type은 Text의 목록들을 들고 있음

    public abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object // enum을 통째로 넘겨받으면 목록들이랑 실제 Object랑 매핑해주는 함수 // <T>, Button Component, Text Component를 들고있는 Object를 찾기위해 힌트를 주기 위함
    {
        string[] names = Enum.GetNames(type); // type으로 넘겨받은 enum에 속한 enum값들을 string으로 변환해서 배열로 저장
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // enum값의 개수만큼 UnityEngine.Object의 배열을 만들어줌 // UnityEngine.Object에는 모든 Object들(Text, Button 등)을 저장 가능
        _objects.Add(typeof(T), objects); // Key값에는 Type(Text, Button 등)를 넣어주고, Value값에는 objects를 넣어주기 // 여기까지하면 objects는 빈 배열 // object들을 찾아서 넣어줘야함

        // names에 들어있는 object의 이름들로 실제 object를 찾아 연결해주는 과정
        for (int i = 0; i < names.Length; i++) // names에 들어있는 object들의 이름들의 수만큼 반복
        {
            if (typeof(T) == typeof(GameObject)) // T타입이 GameObject라면
            {
                objects[i] = Util.FindChild(gameObject, names[i], true); // GameObject 전용 버전의 FindChild 함수 호출
            }
            else // T타입이 Component라면
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true); // gameObject는 UI_Button Script를 포함하고있는 UI_Button Object를 의미 // Texts - pointText의 경우에는 gameObject의 자식의 자식이기 때문에 재귀는 true여야함
            }

            if (objects[i] == null) // 해당 Object를 못찾았으면
            {
                Debug.Log($"Failed to bind({names[i]}!"); // names[i]라는 이름을 가진, T타입의 component를 들고있는, Object를 못찾았다는 의미
            }
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object // 인덱스에 해당하는 T타입의 component를 가진 Object반환
    {
        UnityEngine.Object[] objects = null; // 추출한 Value들을 저장하기 위한 Object배열
        if (_objects.TryGetValue(typeof(T), out objects) == false) // TryGetValue로 Key값을 이용해 Value 추출하는 것을 실패했다면
        {
            return null; // null을 return
        }

        return objects[idx] as T; // Object배열에서 인덱스에 해당하는 Value만 추출 // T타입으로 캐스팅해서(원래는 Object타입으로 저장되어있었기 때문) return
    }

    /*protected Text GetText(int idx)
    {
        return Get<Text>(idx);
    }*/

    protected TMP_Text GetText(int idx) // 자주 사용하기 때문에 간편화 시킴
    {
        return Get<TMP_Text>(idx);
    }

    protected Button GetButton(int idx) // 자주 사용하기 때문에 간편화 시킴
    {
        return Get<Button>(idx);
    }

    protected Image GetImage(int idx) // 자주 사용하기 때문에 간편화 시킴
    {
        return Get<Image>(idx);
    }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) // UI Event를 추가해주는 함수 정의 // (GameObject, 구독시킬 함수(CallBack), 구독을 받을 이벤트(기본은 클릭으로 설정))
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go); // UI_EventHandler component를 찾고 없으면 추가해서, evt에 받아옴

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action; // 중복 구독 방지
                evt.OnClickHandler += action; // action을 OnClickHandler에 구독시킴
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action; // 중복 구독 방지
                evt.OnDragHandler += action; // action을 OnDragHandler에 구독시킴
                break;
        }
    }
}