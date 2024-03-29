using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value> // Key값과, Value값을 받아서
{
    Dictionary<Key, Value> MakeDict(); // Dictionary<Key, Value>값을 반환하는 MakeDict() 메소드를 구현해야하는 인터페이스 작성
}

public class ScriptManager
{
    public List<Script> Scripts = new List<Script>();
    public Dictionary<int, Script> ScriptDict { get; private set; } = new Dictionary<int, Script>();

    public void Init()
    {
        ScriptData scripts = LoadJson<ScriptData, int, Script>("ScriptData");

        Scripts = scripts.Scripts;

        ScriptDict = scripts.MakeDict(); // JSON에서 데이터를 파싱해서 저장하고, Dictionary로 만들어 주기
        

        Debug.Log("ScriptDict initialized with " + ScriptDict.Count + " items.");
        Debug.Log("Scripts list initialized with " + Scripts.Count + " items.");
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value> // JSON에서 데이터를 로드하는 메소드
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}"); // 주소에서 JSON데이터를 TextAsset으로 받아오기
      
        Loader loader = JsonUtility.FromJson<Loader>(textAsset.text);
        
        

        return loader; // JSON에서 데이터를 파싱해서, Loader에 해당하는(List를 가진) class와 하위 List의 아이템 class에 알맞게 넣어줌
    }
}