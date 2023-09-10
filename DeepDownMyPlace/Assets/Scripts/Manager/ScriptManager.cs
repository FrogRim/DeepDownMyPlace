using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value> // Key����, Value���� �޾Ƽ�
{
    Dictionary<Key, Value> MakeDict(); // Dictionary<Key, Value>���� ��ȯ�ϴ� MakeDict() �޼ҵ带 �����ؾ��ϴ� �������̽� �ۼ�
}

public class ScriptManager
{
    public List<Script> Scripts = new List<Script>();
    public Dictionary<int, Script> ScriptDict { get; private set; } = new Dictionary<int, Script>();

    public void Init()
    {
        ScriptData scripts = LoadJson<ScriptData, int, Script>("ScriptData");

        Scripts = scripts.Scripts;

        ScriptDict = scripts.MakeDict(); // JSON���� �����͸� �Ľ��ؼ� �����ϰ�, Dictionary�� ����� �ֱ�
        

        Debug.Log("ScriptDict initialized with " + ScriptDict.Count + " items.");
        Debug.Log("Scripts list initialized with " + Scripts.Count + " items.");
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value> // JSON���� �����͸� �ε��ϴ� �޼ҵ�
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}"); // �ּҿ��� JSON�����͸� TextAsset���� �޾ƿ���
      
        Loader loader = JsonUtility.FromJson<Loader>(textAsset.text);
        
        

        return loader; // JSON���� �����͸� �Ľ��ؼ�, Loader�� �ش��ϴ�(List�� ����) class�� ���� List�� ������ class�� �˸°� �־���
    }
}