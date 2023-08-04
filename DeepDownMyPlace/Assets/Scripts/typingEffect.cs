using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class typingEffect : MonoBehaviour
{
    public TextMeshProUGUI[] texts;

    string[] tx = { "Deep Down My Place", "Start"};


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnType());
    }

    IEnumerator OnType()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < tx.Length; i++)
        {
            for (int j = 0; j < tx[i].Length+1; j++)
            {
                texts[i].text = tx[i].Substring(0, j);
                //���ʿ��ٰ� Ÿ���μҸ� �ֱ�
                yield return new WaitForSeconds(0.15f);
            }
        }
    }
}
