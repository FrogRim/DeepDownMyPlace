using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager 사용하기 위함

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

       
    }

    // Update is called once per frame
    
    public void LoadGame()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }
    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
        //throw new System.NotImplementedException();
    }
}