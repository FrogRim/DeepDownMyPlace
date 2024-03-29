using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance
    {
        get
        {
            Init();
            return s_instance;
        }
    }

    ScriptManager _script = new ScriptManager(); // DataManager 추가
    public static ScriptManager Script // DataManager를 사용하고 싶으면 Managers.Data로 접근
    {
        get
        {
            return Instance._script;
        }
    }

    InputManager _input = new InputManager();
    public static InputManager Input
    {
        get
        {
            return Instance._input;
        }
    }

    PoolManager _pool = new PoolManager();
    public static PoolManager Pool
    {
        get
        {
            return Instance._pool;
        }
    }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource
    {
        get
        {
            return Instance._resource;
        }
    }

    SceneManagerEx _scene = new SceneManagerEx();
    public static SceneManagerEx Scene
    {
        get
        {
            return Instance._scene;
        }
    }

    SoundManager _sound = new SoundManager();
    public static SoundManager Sound
    {
        get
        {
            return Instance._sound;
        }
    }

    UIManager _ui = new UIManager();
    public static UIManager UI
    {
        get
        {
            return Instance._ui;
        }
    }

  


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        //_input.OnUpdate();
        Input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            //s_instance._script.Init(); // ScriptManager 초기화
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
    }

    public static void Clear() // Scene이 바뀔때 초기화
    {
       
        Input.Clear();
        Scene.Clear();
        Sound.Clear();
        UI.Clear();
        Pool.Clear(); // 다른 곳에서 pooling된 객체를 사용할 수도 있으니, 맨 마지막에 Clear
    }
}