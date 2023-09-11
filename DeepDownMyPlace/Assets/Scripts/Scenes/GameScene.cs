using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScene : BaseScene
{

    public TextMeshProUGUI texts;
    public List<string> tx = new List<string>();

    void Awake()
    {
        // Managers ��ũ��Ʈ�� �ʱ�ȭ�� �Ϸ�Ǿ����� Ȯ��
        Managers.Script.Init();

        // Managers.Script.ScriptDict�� null�� �ƴϰ� �׸��� �ϳ� �̻� �ִ��� Ȯ���Ͻʽÿ�.
        if (Managers.Script.ScriptDict != null && Managers.Script.ScriptDict.Count > 0)
        {
            foreach (KeyValuePair<int, Script> value in Managers.Script.ScriptDict)
            {
                tx.Add(value.Value.script);
                
            }
        }
        else
        {
            Debug.LogError("Managers.Script.ScriptDict�� ��� �ְų� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;


        

        // Managers.UI.ShowSceneUI<UI_Inven>();


    }


    public override void Clear()
    {
        //throw new System.NotImplementedException();
    }

    
}