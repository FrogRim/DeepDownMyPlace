using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    public TMP_Text _Text;

    public Text text;

    public TextMeshProUGUI text2;

    string dialogue;

    public string[] tutorialDialogue;

    public string[] dialogues;

    // Start is called before the first frame update
    void Start()
    {
        startTalk(tutorialDialogue);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Type(string talk)
    {
        //초기화
        text.text = null;


        text2.text = talk;
        TMPDOText(text2, 1f);

        text.DOText(talk, 1f); // 모든 문자가 출력되는데 걸리는 시간 - 1초

        //다음대사 딜레이
        yield return new WaitForSeconds(1.5f);
        NextTalk();

    }

    //다음 대사를 출력하기위한 정수
    [SerializeField]
    int talknum;

    public void startTalk(string[] talks)
    {
        dialogues = talks;

        //talknum번째 대사 출력
        StartCoroutine(Type(dialogues[talknum]));
    }

    public void NextTalk()
    {
        _Text.text = null;

        //다음 배열을 출력하기위해 +1
        talknum++;

        // 배열 안 모든 문자를 출력했으면 끝내기
        if (talknum == dialogues.Length)
        {
            EndTalk();
            return;
        }

        StartCoroutine(Type(dialogues[talknum]));
    }

    public static void TMPDOText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration);
    }
    public void EndTalk()
    {
        //talknum 초기화
        talknum = 0;

    }
}