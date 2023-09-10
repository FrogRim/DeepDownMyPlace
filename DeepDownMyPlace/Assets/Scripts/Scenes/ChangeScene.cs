using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager 사용하기 위함

public class ChangeScene : BaseScene
{
    public int scene_num = 999;
    protected override void Init()
    {
        base.Init();
        

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("OOO");
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.GetComponent<Collider2D>());
        if (Input.GetKeyDown(KeyCode.F))
        {
            LoadGame();
        }
    }
   
  
    // Update is called once per frame

    public void LoadGame()
    {
       

        switch (scene_num)
        {
           
            case 4:
                Managers.Scene.LoadScene(Define.Scene.Library);
                break;
            case 5:
                Managers.Scene.LoadScene( Define.Scene.Class1);
                break;
            case 6:
                Managers.Scene.LoadScene( Define.Scene.Class2);
                break;
            case 7:
                Managers.Scene.LoadScene( Define.Scene.Class3);
                break;
            case 8:
                Managers.Scene.LoadScene( Define.Scene.Class4);
                break;
            case 9:
                Managers.Scene.LoadScene( Define.Scene.Class5);
                break;
            case 10:
                Managers.Scene.LoadScene( Define.Scene.Science);
                break;
            case 11:
                Managers.Scene.LoadScene( Define.Scene.Music);
                break;

        }

    }
    public override void Clear()
    {
        //Debug.Log("LoginScene Clear!");
        //throw new System.NotImplementedException();
    }
}
