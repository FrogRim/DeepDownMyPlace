using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*public AudioClip audioClip; // audioClip�� ���� ����
    public AudioClip audioClip2; // audioClip2�� ���� ����*/

    int i = 0;

    private void OnTriggerEnter(Collider other)
    {
        /*AudioSource audio = GetComponent<AudioSource>(); // AudioSource Component ����
        //audio.PlayClipAtPoint( , ); // AudioClip�� ��ġ�� ���ڷ� �ް�, �ش� ��ġ���� AudioClip�� ����ϰ� �ϴ� �ڵ�
        audio.PlayOneShot(audioClip); // �� ���� �����Ŵ
        audio.PlayOneShot(audioClip2); // �� ���� �����Ŵ
        float lifeTime = Mathf.Max(audioClip.length, audioClip2.length); // �� ���� �� �� ������ �ð��� lifeTime�� ����
        GameObject.Destroy(gameObject, lifeTime); // lifeTime �ڿ� Object ����� // lifeTime ��� 0.25f�� ���� �ð��� ������, �Ҹ��� �߰��� �����*/

        /*// �� �ڵ带 Manager�� ó��
        Managers.Sound.Play("UnityChan/univ0001");
        Managers.Sound.Play("UnityChan/univ0002");*/

        // Bgm���� �ݺ�����ϰ� ����
        i++;

        if (i % 2 == 0)
        {
            Managers.Sound.Play("UnityChan/univ0001", Define.Sound.Bgm);
        }
        else
        {
            Managers.Sound.Play("UnityChan/univ0002", Define.Sound.Bgm);
        }
    }
}