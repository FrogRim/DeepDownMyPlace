using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager 사용하기 위함

public class SceneManagerEx // SceneManager은 Unity가 기본으로 사용하는 이름이기 때문에 Ex를 붙임
{
    public BaseScene CurrentScene // 외부에서 현재 Scene을 참조할 때 사용
    {
        get
        {
            //return GameObject.FindObjectOfType(typeof(BaseScene)) as BaseScene; // BaseScene Script Component를 가지고있는 @Scene Object를 찾고, BaseScene 타입으로 캐스팅
            return GameObject.FindObjectOfType<BaseScene>(); // 위 코드와 같은 기능
        }
    }

    public void LoadScene(Define.Scene type) // string이 아닌 enum값으로 Scene을 불러오려는 메소드
    {
        Managers.Clear(); // Scene이 바뀔때 초기화
        SceneManager.LoadScene(GetSceneName(type)); // Define.Scene에서 enum값을 가져와 Scene을 불러오기
    }

    string GetSceneName(Define.Scene type) // enum값을 string으로 바꿔주는 메소드
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);

        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear(); // 현재 Scene의 Clear() 호출
    }
}