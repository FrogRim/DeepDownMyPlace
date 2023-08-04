using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager ����ϱ� ����

public class SceneManagerEx // SceneManager�� Unity�� �⺻���� ����ϴ� �̸��̱� ������ Ex�� ����
{
    public BaseScene CurrentScene // �ܺο��� ���� Scene�� ������ �� ���
    {
        get
        {
            //return GameObject.FindObjectOfType(typeof(BaseScene)) as BaseScene; // BaseScene Script Component�� �������ִ� @Scene Object�� ã��, BaseScene Ÿ������ ĳ����
            return GameObject.FindObjectOfType<BaseScene>(); // �� �ڵ�� ���� ���
        }
    }

    public void LoadScene(Define.Scene type) // string�� �ƴ� enum������ Scene�� �ҷ������� �޼ҵ�
    {
        Managers.Clear(); // Scene�� �ٲ� �ʱ�ȭ
        SceneManager.LoadScene(GetSceneName(type)); // Define.Scene���� enum���� ������ Scene�� �ҷ�����
    }

    string GetSceneName(Define.Scene type) // enum���� string���� �ٲ��ִ� �޼ҵ�
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);

        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear(); // ���� Scene�� Clear() ȣ��
    }
}