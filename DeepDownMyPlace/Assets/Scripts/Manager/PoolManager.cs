using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    // region���� ������ �ڵ带 ������ ��� �ϱ� // ��������� ������ �߰��� ���� �ƴ�
    #region Pool
    class Pool // ��ü �̸����� Pool�� ����(UnityChan_Root, Tank_Root ��)
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>(); // Pool�� ����ϰ��ִ� ��ü���� �׾Ƶδ� ��

        public void Init(GameObject original, int count)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root"; // ��ü�̸�_Root Object ������ֱ�

            for (int i = 0; i < count; i++) // count Ƚ����ŭ Object ����
            {
                Push(Create());
            }
        }

        Poolable Create() // Pooling�� ������� ������ Object�� ������ִ� �޼ҵ�
        {
            GameObject go = Object.Instantiate<GameObject>(Original); // ����(Original)�� �ָ� ���纻�� ����� go�� ����
            go.name = Original.name; // (Clone)�̶�� ���ڿ� �����ϱ� ����

            return go.GetOrAddComponent<Poolable>(); // Poolable Component�� ã�ų� �߰� �� return
        }

        public void Push(Poolable poolable) // ���ÿ� �ֱ����� ó���� �͵� ó�� // �ٷ� _poolStack.Push()�� �����ʰ�, Push�� ���� ���ÿ� �׾���
        {
            if (poolable == null) // poolable�� ��������� // ���� ���� �˻�
            {
                return; // �׳� return
            }

            poolable.transform.parent = Root; // ��ü�̸�_Root�� �θ�� ���� // ��ü�̸�_Root �Ʒ��� ��ü�� �־���
            poolable.gameObject.SetActive(false); // Object ���� ���� ���� // Object�� ������ ����
            poolable.IsUsing = false; // ����ϰ� ���� �ʾ� false

            _poolStack.Push(poolable); // ���ÿ� �־��ֱ�
        }

        public Poolable Pop(Transform parent) // ���ÿ��� ���������� ó���� �͵� ó�� // �ٷ� _poolStack.Pop()�� �����ʰ�, Pop�� ���� ���ÿ��� ������
        {
            Poolable poolable;

            if (_poolStack.Count > 0) // ���ÿ� ������ �� �ִ� ��ü�� �ִٸ�
            {
                poolable = _poolStack.Pop(); // ���ÿ��� ��������
            }
            else // �ϳ��� ���ٸ� // ������� ��ü�� ���ٸ�
            {
                poolable = Create(); // ��ü ����
            }

            poolable.gameObject.SetActive(true); // Object ���� ���� ���� // Object�� �ٽ� ���̴� ����

            // DontDestroyOnLoad ���� �뵵
            if (parent == null) // parent�� null�̸� // null�̸� �θ� ���� �������� ���� // �� �ڵ带 �������� �ʰ� �ٷ� �����ڵ�� �Ѿ�� parent�� null�̱� ������ Scene �Ʒ��� Object�� ��ġ�� �� ������, ������ Object�� DontDestroyOnLoad �ȿ� �ִ� @Pool_root �Ʒ��� �־��� ������, DontDestroyOnLoad �Ʒ��� �̵��� - �׷��� �ڵ带 ����)
            {
                poolable.transform.parent = Managers.Scene.CurrentScene.transform; // @Scene Object ������ �ѹ� �̵���Ű��
            }

            poolable.transform.parent = parent; // Pool���� ������ ��ü�� �����ϰ� �� �θ��� ��ġ�� ����
            poolable.IsUsing = true; // ����ϰ� ������ true

            return poolable; // poolable ��ȯ
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>(); // ��ü �̸��� �ش��ϴ� Pool�� Dictionary�� �����ֱ� // �������� Pool�� ����

    Transform _root; // GameObject�� ���� ��

    public void Init()
    {
        if (_root == null) // _root�� ����ִٸ�
        {
            _root = new GameObject { name = "@Pool_Root" }.transform; // @Pool_Root Object ����� // ��� Pooling ��ü�� ��Ʈ(����)
            Object.DontDestroyOnLoad(_root); // _root ���� ����
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool(); // Pool Ŭ���� ����
        pool.Init(original, count); // original�� count �Ѱ��ְ�, Pool Ŭ������ Init() ����
        pool.Root.parent = _root; // Pool�� �θ� _root�� ���� // @Pool_Root �Ʒ��� ��ü�̸�_Root�� �ְԵ�

        _pool.Add(original.name, pool); // Dictionary _pool�� �߰�
    }

    public void Push(Poolable poolable) // Pool �ȿ� ��ü�� �ִ�(���Ƿ� ������) ���
    {
        string name = poolable.gameObject.name;

        if (_pool.ContainsKey(name) == false) // _pool�� name�̶�� Key�� ���ٸ� // ���� �������� ���
        {
            GameObject.Destroy(poolable.gameObject); // Pool�� ���� �ʰ�, �׳� ����

            return; // �׳� return
        }

        _pool[name].Push(poolable); // name�� �ش��ϴ� Pool�� ã�Ƽ�, poolable�� �־��� // Pool.Push
    }

    public Poolable Pop(GameObject original, Transform parent = null) // Pool���� ��ü�� ������(Scene�� �����ϴ�) ���
    {
        if (_pool.ContainsKey(original.name) == false) // _pool�� original.name�̶�� Key�� ���ٸ� // �� ó�� Pop�� ȣ������ ��쿣 Pool�� ���� ����
        {
            CreatePool(original); // Pool�� ���� �����
        }

        return _pool[original.name].Pop(parent); // original.name�� �ش��ϴ� Pool�� ã�Ƽ�, parent ��ġ�� ���ÿ��� �������� // Pool.Pop
    }

    public GameObject GetOriginal(string name) // ���� ��ü�� Pool���� �޾ƿ���(ã��) �޼ҵ�
    {
        if (_pool.ContainsKey(name) == false) // _pool�� name�̶�� Key�� ���ٸ�
        {
            return null; // null�� return
        }

        return _pool[name].Original; // name�� �ش��ϴ� Pool�� ã�Ƽ�, ���� ��ü ��������
    }

    public void Clear()
    {
        foreach (Transform child in _root) // _root�� �ڽ��� ���� ���鼭
        {
            GameObject.Destroy(child.gameObject); // �ڽ� ����
        }

        _pool.Clear(); // _pool Dictionary�� �ʱ�ȭ
    }
}