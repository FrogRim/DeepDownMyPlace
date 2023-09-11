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
        //�ʱ�ȭ
        text.text = null;


        text2.text = talk;
        TMPDOText(text2, 1f);

        text.DOText(talk, 1f); // ��� ���ڰ� ��µǴµ� �ɸ��� �ð� - 1��

        //������� ������
        yield return new WaitForSeconds(1.5f);
        NextTalk();

    }

    //���� ��縦 ����ϱ����� ����
    [SerializeField]
    int talknum;

    public void startTalk(string[] talks)
    {
        dialogues = talks;

        //talknum��° ��� ���
        StartCoroutine(Type(dialogues[talknum]));
    }

    public void NextTalk()
    {
        _Text.text = null;

        //���� �迭�� ����ϱ����� +1
        talknum++;

        // �迭 �� ��� ���ڸ� ��������� ������
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
        //talknum �ʱ�ȭ
        talknum = 0;

    }
}