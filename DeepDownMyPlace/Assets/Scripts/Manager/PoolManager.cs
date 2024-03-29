using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    // region으로 설정해 코드를 접었다 폈다 하기 // 기능적으로 무엇을 추가한 것은 아님
    #region Pool
    class Pool // 객체 이름마다 Pool이 존재(UnityChan_Root, Tank_Root 등)
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>(); // Pool에 대기하고있는 객체들을 쌓아두는 곳

        public void Init(GameObject original, int count)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root"; // 객체이름_Root Object 만들어주기

            for (int i = 0; i < count; i++) // count 횟수만큼 Object 생성
            {
                Push(Create());
            }
        }

        Poolable Create() // Pooling과 관계없는 실제로 Object를 만들어주는 메소드
        {
            GameObject go = Object.Instantiate<GameObject>(Original); // 원본(Original)을 주면 복사본을 만들어 go에 저장
            go.name = Original.name; // (Clone)이라는 문자열 제거하기 위함

            return go.GetOrAddComponent<Poolable>(); // Poolable Component를 찾거나 추가 후 return
        }

        public void Push(Poolable poolable) // 스택에 넣기전에 처리할 것들 처리 // 바로 _poolStack.Push()를 하지않고, Push를 통해 스택에 쌓아줌
        {
            if (poolable == null) // poolable이 비어있으면 // 안전 차원 검사
            {
                return; // 그냥 return
            }

            poolable.transform.parent = Root; // 객체이름_Root를 부모로 지정 // 객체이름_Root 아래에 객체를 넣어줌
            poolable.gameObject.SetActive(false); // Object 불이 꺼진 상태 // Object가 숨겨진 상태
            poolable.IsUsing = false; // 사용하고 있지 않아 false

            _poolStack.Push(poolable); // 스택에 넣어주기
        }

        public Poolable Pop(Transform parent) // 스택에서 꺼내기전에 처리할 것들 처리 // 바로 _poolStack.Pop()을 하지않고, Pop을 통해 스택에서 꺼내옴
        {
            Poolable poolable;

            if (_poolStack.Count > 0) // 스택에 꺼내올 수 있는 객체가 있다면
            {
                poolable = _poolStack.Pop(); // 스택에서 꺼내오기
            }
            else // 하나도 없다면 // 대기중인 객체가 없다면
            {
                poolable = Create(); // 객체 생성
            }

            poolable.gameObject.SetActive(true); // Object 불이 켜진 상태 // Object가 다시 보이는 상태

            // DontDestroyOnLoad 해제 용도
            if (parent == null) // parent가 null이면 // null이면 부모가 따로 존재하지 않음 // 이 코드를 실행하지 않고 바로 다음코드로 넘어가도 parent가 null이기 때문에 Scene 아래에 Object가 배치될 것 같지만, 기존에 Object가 DontDestroyOnLoad 안에 있는 @Pool_root 아래에 있었기 때문에, DontDestroyOnLoad 아래로 이동함 - 그래서 코드를 실행)
            {
                poolable.transform.parent = Managers.Scene.CurrentScene.transform; // @Scene Object 밑으로 한번 이동시키기
            }

            poolable.transform.parent = parent; // Pool에서 꺼내온 객체가 존재하게 될 부모의 위치를 설정
            poolable.IsUsing = true; // 사용하고 있으니 true

            return poolable; // poolable 반환
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>(); // 객체 이름과 해당하는 Pool을 Dictionary로 묶어주기 // 여러개의 Pool이 존재

    Transform _root; // GameObject로 만들어도 됨

    public void Init()
    {
        if (_root == null) // _root가 비어있다면
        {
            _root = new GameObject { name = "@Pool_Root" }.transform; // @Pool_Root Object 만들기 // 모든 Pooling 객체의 루트(대기실)
            Object.DontDestroyOnLoad(_root); // _root 삭제 금지
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool(); // Pool 클래스 생성
        pool.Init(original, count); // original과 count 넘겨주고, Pool 클래스의 Init() 실행
        pool.Root.parent = _root; // Pool의 부모를 _root로 설정 // @Pool_Root 아래에 객체이름_Root가 있게됨

        _pool.Add(original.name, pool); // Dictionary _pool에 추가
    }

    public void Push(Poolable poolable) // Pool 안에 객체를 넣는(대기실로 보내는) 기능
    {
        string name = poolable.gameObject.name;

        if (_pool.ContainsKey(name) == false) // _pool에 name이라는 Key가 없다면 // 정말 예외적인 경우
        {
            GameObject.Destroy(poolable.gameObject); // Pool에 넣지 않고, 그냥 삭제

            return; // 그냥 return
        }

        _pool[name].Push(poolable); // name에 해당하는 Pool을 찾아서, poolable을 넣어줌 // Pool.Push
    }

    public Poolable Pop(GameObject original, Transform parent = null) // Pool에서 객체를 꺼내는(Scene에 등장하는) 기능
    {
        if (_pool.ContainsKey(original.name) == false) // _pool에 original.name이라는 Key가 없다면 // 맨 처음 Pop을 호출했을 경우엔 Pool이 없는 상태
        {
            CreatePool(original); // Pool을 새로 만들기
        }

        return _pool[original.name].Pop(parent); // original.name에 해당하는 Pool을 찾아서, parent 위치로 스택에서 꺼내오기 // Pool.Pop
    }

    public GameObject GetOriginal(string name) // 원본 객체를 Pool에서 받아오는(찾는) 메소드
    {
        if (_pool.ContainsKey(name) == false) // _pool에 name이라는 Key가 없다면
        {
            return null; // null을 return
        }

        return _pool[name].Original; // name에 해당하는 Pool을 찾아서, 원본 객체 꺼내오기
    }

    public void Clear()
    {
        foreach (Transform child in _root) // _root의 자식을 각각 돌면서
        {
            GameObject.Destroy(child.gameObject); // 자식 삭제
        }

        _pool.Clear(); // _pool Dictionary도 초기화
    }
}