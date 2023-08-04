using System; // [Serializable] ����ϱ� ����
using System.Collections;
using System.Collections.Generic;
//using System.Linq; // ToDictionary ����ϱ� ����
using UnityEngine;

#region Stat
[Serializable]
public class Stat // stats ����Ʈ�� �� �����۵�
{
    // public���� ����� ���� �ʴٸ� SerializeField �̿�
    public int level;
    public int hp;
    public int attack;
    // ���� �̸��� JSON���ϰ� �Ȱ��� ���������
}

[Serializable]
public class StatData : ILoader<int, Stat> // stats ����Ʈ // stats ����Ʈ�� �޾ƿͼ� List�� �����ϰ�, Dictionary�� ��ȯ�ϴ� �۾��� �ϳ��� Ŭ�������� �� ����
{
    public List<Stat> stats = new List<Stat>();

    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        //stats.ToDictionary(); // IOS���� ���װ� ���� �Ʒ� ��� �̿�
        foreach (Stat stat in stats) // stats ����Ʈ�� ����
        {
            dict.Add(stat.level, stat); // dict�� �� Stat���� �߰�
        }

        return dict; // dict�� return
        //throw new NotImplementedException();
    }
}
#endregion