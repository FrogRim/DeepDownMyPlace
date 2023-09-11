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
        // Managers 스크립트의 초기화가 완료되었는지 확인
        Managers.Script.Init();

        // Managers.Script.ScriptDict이 null이 아니고 항목이 하나 이상 있는지 확인하십시오.
        if (Managers.Script.ScriptDict != null && Managers.Script.ScriptDict.Count > 0)
        {
            foreach (KeyValuePair<int, Script> value in Managers.Script.ScriptDict)
            {
                tx.Add(value.Value.script);
                
            }
        }
        else
        {
            Debug.LogError("Managers.Script.ScriptDict가 비어 있거나 초기화되지 않았습니다.");
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