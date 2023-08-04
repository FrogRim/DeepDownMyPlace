using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Minimap : MonoBehaviour
{

    //맵 범위
    [SerializeField]
    private Transform top;  //맵의 천장
    [SerializeField]
    private Transform bottom;   //맵의 바닥
    [SerializeField]
    private Transform left; //맵의 가장 왼쪽
    [SerializeField]
    private Transform right;    //맵의 가장 오른쪽
    
    //이미지
    [SerializeField]
    private Image minimapImage;
    [SerializeField]
    private Image minimapPlayerImage;

    //타겟 설정
    [SerializeField]
    private Transform targetPlayer;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        //맵 크기만한 2d벡터
        Vector2 mapArea = new Vector2(Vector3.Distance(left.position, right.position), Vector3.Distance(bottom.position, top.position));
        //타켓 캐릭터의 위치 계산
        Vector2 charPos = new Vector2(Vector3.Distance(left.position, new Vector3(targetPlayer.transform.position.x, 0f, 0f)),
            Vector3.Distance(bottom.position, new Vector3(0f, targetPlayer.transform.position.y, 0f)));
        Vector2 normalPos = new Vector2(charPos.x / mapArea.x, charPos.y / mapArea.y);

        minimapPlayerImage.rectTransform.anchoredPosition = new Vector2(minimapImage.rectTransform.sizeDelta.x * normalPos.x, minimapImage.rectTransform.sizeDelta.y * normalPos.y);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
