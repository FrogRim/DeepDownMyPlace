using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value> // Key����, Value���� �޾Ƽ�
{
    Dictionary<Key, Value> MakeDict(); // Dictionary<Key, Value>���� ��ȯ�ϴ� MakeDict() �޼ҵ带 �����ؾ��ϴ� �������̽� �ۼ�
}

public class ScriptManager
{
    public Dictionary<int, Script> ScriptDict { get; private set; } = new Dictionary<int, Script>();

    public void Init()
    {
        ScriptDict = LoadJson<ScriptData, int, Script>("ScriptData").MakeDict(); // JSON���� �����͸� �Ľ��ؼ� �����ϰ�, Dictionary�� ����� �ֱ�
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value> // JSON���� �����͸� �ε��ϴ� �޼ҵ�
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}"); // �ּҿ��� JSON�����͸� TextAsset���� �޾ƿ���
        return JsonUtility.FromJson<Loader>(textAsset.text); // JSON���� �����͸� �Ľ��ؼ�, Loader�� �ش��ϴ�(List�� ����) class�� ���� List�� ������ class�� �˸°� �־���
    }
}