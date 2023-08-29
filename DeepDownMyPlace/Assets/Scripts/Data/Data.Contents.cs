using System; // [Serializable] 사용하기 위함
using System.Collections;
using System.Collections.Generic;
//using System.Linq; // ToDictionary 사용하기 위함
using UnityEngine;

#region Script
[Serializable]
public class Script // Scripts 리스트의 각 아이템들
{
    // public으로 만들고 싶지 않다면 SerializeField 이용
    public int number;
    public int name;
    public string script;
    // 변수 이름을 JSON파일과 똑같이 맞춰줘야함
}

[Serializable]
public class ScriptData : ILoader<int, Script> // Scripts 리스트 // Scripts 리스트를 받아와서 List로 저장하고, Dictionary로 변환하는 작업도 하나의 클래스에서 다 해줌
{
    public List<Script> Scripts = new List<Script>();

    public Dictionary<int, Script> MakeDict()
    {
        Dictionary<int, Script> dict = new Dictionary<int, Script>();
        //Scripts.ToDictionary(); // IOS에서 버그가 많아 아래 방법 이용
        foreach (Script Script in Scripts) // Scripts 리스트를 돌며
        {
            dict.Add(Script.number, Script); // dict에 각 Script정보 추가
        }

        return dict; // dict를 return
        //throw new NotImplementedException();
    }
}
#endregion