using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScene : BaseScene
{

    public TextMeshProUGUI texts;
    public List<string> tx = new List<string>();


    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;


        foreach (var value in Managers.Script.ScriptDict)
        {
            tx.Add(value.Value.script);
            Debug.Log(value.Value.script);
        }
        

       // Managers.UI.ShowSceneUI<UI_Inven>();

        StartCoroutine(OnType());
    }


    public override void Clear()
    {
        //throw new System.NotImplementedException();
    }

    IEnumerator OnType()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < tx.Count; i++)
        {
            for (int j = 0; j < tx[i].Length + 1; j++)
            {
                texts.text = tx[i].Substring(0, j);
                //이쪽에다가 타이핑소리 넣기
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}