using System; // [Serializable] ����ϱ� ����
using System.Collections;
using System.Collections.Generic;
//using System.Linq; // ToDictionary ����ϱ� ����
using UnityEngine;

#region Script
[Serializable]
public class Script // Scripts ����Ʈ�� �� �����۵�
{
    // public���� ����� ���� �ʴٸ� SerializeField �̿�
    public int number;
    public string name;
    public string script;
    // ���� �̸��� JSON���ϰ� �Ȱ��� ���������
}

[Serializable]
public class ScriptData : ILoader<int, Script> // Scripts ����Ʈ // Scripts ����Ʈ�� �޾ƿͼ� List�� �����ϰ�, Dictionary�� ��ȯ�ϴ� �۾��� �ϳ��� Ŭ�������� �� ����
{
    public List<Script> Scripts = new List<Script>();

    public ScriptData()
    {
        Scripts = new List<Script>(); // �����ڿ��� �ʱ�ȭ
    }


    public Dictionary<int, Script> MakeDict()
    {
        
        Dictionary<int, Script> dict = new Dictionary<int, Script>();
        //Scripts.ToDictionary(); // IOS���� ���װ� ���� �Ʒ� ��� �̿�
        foreach (Script Script in Scripts) // Scripts ����Ʈ�� ����
        {
            dict.Add(Script.number, Script); // dict�� �� Script���� �߰�
            
        }

        return dict; // dict�� return
        //throw new NotImplementedException();
    }
}
#endregion