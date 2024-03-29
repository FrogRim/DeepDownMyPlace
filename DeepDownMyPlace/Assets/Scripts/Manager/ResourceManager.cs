using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object // 경로에서 Object를 불러와서 넣기 // 일반화 // T는 Object면 된다는 조건 // 단순 랩핑
    {
        // 찾으려는 원본 객체가 Pool Dictionary에 존재하면 바로 사용
        if (typeof(T) == typeof(GameObject)) // T타입이 GameObject라면
        {
            // path로부터 이름 추출
            string name = path;
            int index = name.LastIndexOf('/'); // 마지막 /의 인덱스를 찾고
            if (index >= 0) // 인덱스를 찾았다면
            {
                name = name.Substring(index + 1); // / 한칸 뒤 부터 이름을 받아 저장
            }

            GameObject go = Managers.Pool.GetOriginal(name); // name 객체를 Pool Dictionary에서 찾기
            if (go != null) // 객체를 찾았다면
            {
                return go as T; // 객체를 T타입으로 반환 // 물론 여기서 T는 GameObject
            }
        }

        // 객체를 찾지 못했다면
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null) // Instance 생성 메소드 // 메소드 랩핑
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null) // original을 찾을 수 없었다면
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null; // null 반환
        }

        // Pooling의 대상이라면
        if (original.GetComponent<Poolable>() != null) // Poolable Component를 가지고 있다면 // 즉, pooling의 대상이라면
        {
            return Managers.Pool.Pop(original, parent).gameObject; // Pool에서 꺼내오기
        }

        // Pooling의 대상이 아니였다면
        GameObject go = Object.Instantiate(original, parent);
        /*int index = go.name.IndexOf("(Clone)"); // (Clone) 문자열이 있는지 찾고, 그 인덱스를 반환
        if (index > 0) // (Clone) 문자열이 있었다면
        {
            go.name = go.name.Substring(0, index); // 0번부터 index 전까지 잘라서 이름을 저장
        }*/
        go.name = original.name; // 이렇게 해도 (Clone)이 안붙고 Object가 생성됨

        return go;
    }

    public void Destroy(GameObject go, float f = 0.0f) // Object 삭제 메소드
    {
        if (go == null) // 없는 Object였다면
        {
            return; // 그냥 return
        }

        // Pooling의 대상이라면
        Poolable poolable = go.GetComponent<Poolable>(); // Poolable Component를 가지고있는지 확인
        if (poolable != null) // Poolable Component를 가지고 있다면 // 즉, pooling의 대상이라면
        {
            Managers.Pool.Push(poolable); // Pool에 넣어두기
            return;
        }

        // Pooling의 대상이 아니였다면
        Object.Destroy(go, f);
    }
}