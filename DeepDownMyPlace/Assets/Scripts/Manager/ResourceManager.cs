using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object // ��ο��� Object�� �ҷ��ͼ� �ֱ� // �Ϲ�ȭ // T�� Object�� �ȴٴ� ���� // �ܼ� ����
    {
        // ã������ ���� ��ü�� Pool Dictionary�� �����ϸ� �ٷ� ���
        if (typeof(T) == typeof(GameObject)) // TŸ���� GameObject���
        {
            // path�κ��� �̸� ����
            string name = path;
            int index = name.LastIndexOf('/'); // ������ /�� �ε����� ã��
            if (index >= 0) // �ε����� ã�Ҵٸ�
            {
                name = name.Substring(index + 1); // / ��ĭ �� ���� �̸��� �޾� ����
            }

            GameObject go = Managers.Pool.GetOriginal(name); // name ��ü�� Pool Dictionary���� ã��
            if (go != null) // ��ü�� ã�Ҵٸ�
            {
                return go as T; // ��ü�� TŸ������ ��ȯ // ���� ���⼭ T�� GameObject
            }
        }

        // ��ü�� ã�� ���ߴٸ�
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null) // Instance ���� �޼ҵ� // �޼ҵ� ����
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null) // original�� ã�� �� �����ٸ�
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null; // null ��ȯ
        }

        // Pooling�� ����̶��
        if (original.GetComponent<Poolable>() != null) // Poolable Component�� ������ �ִٸ� // ��, pooling�� ����̶��
        {
            return Managers.Pool.Pop(original, parent).gameObject; // Pool���� ��������
        }

        // Pooling�� ����� �ƴϿ��ٸ�
        GameObject go = Object.Instantiate(original, parent);
        /*int index = go.name.IndexOf("(Clone)"); // (Clone) ���ڿ��� �ִ��� ã��, �� �ε����� ��ȯ
        if (index > 0) // (Clone) ���ڿ��� �־��ٸ�
        {
            go.name = go.name.Substring(0, index); // 0������ index ������ �߶� �̸��� ����
        }*/
        go.name = original.name; // �̷��� �ص� (Clone)�� �Ⱥٰ� Object�� ������

        return go;
    }

    public void Destroy(GameObject go, float f = 0.0f) // Object ���� �޼ҵ�
    {
        if (go == null) // ���� Object���ٸ�
        {
            return; // �׳� return
        }

        // Pooling�� ����̶��
        Poolable poolable = go.GetComponent<Poolable>(); // Poolable Component�� �������ִ��� Ȯ��
        if (poolable != null) // Poolable Component�� ������ �ִٸ� // ��, pooling�� ����̶��
        {
            Managers.Pool.Push(poolable); // Pool�� �־�α�
            return;
        }

        // Pooling�� ����� �ƴϿ��ٸ�
        Object.Destroy(go, f);
    }
}