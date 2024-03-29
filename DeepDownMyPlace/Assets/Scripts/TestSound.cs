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

    /*public AudioClip audioClip; // audioClip에 음원 연결
    public AudioClip audioClip2; // audioClip2에 음원 연결*/

    int i = 0;

    private void OnTriggerEnter(Collider other)
    {
        /*AudioSource audio = GetComponent<AudioSource>(); // AudioSource Component 접근
        //audio.PlayClipAtPoint( , ); // AudioClip과 위치를 인자로 받고, 해당 위치에서 AudioClip을 재생하게 하는 코드
        audio.PlayOneShot(audioClip); // 한 번만 재생시킴
        audio.PlayOneShot(audioClip2); // 한 번만 재생시킴
        float lifeTime = Mathf.Max(audioClip.length, audioClip2.length); // 두 음원 중 긴 음원의 시간을 lifeTime에 저장
        GameObject.Destroy(gameObject, lifeTime); // lifeTime 뒤에 Object 사라짐 // lifeTime 대신 0.25f와 같은 시간을 넣으면, 소리가 중간에 사라짐*/

        /*// 위 코드를 Manager로 처리
        Managers.Sound.Play("UnityChan/univ0001");
        Managers.Sound.Play("UnityChan/univ0002");*/

        // Bgm으로 반복출력하게 설정
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