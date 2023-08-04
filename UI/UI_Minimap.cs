using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
맵 크기를 벡터로 받고 캐릭터 위치를 계산하여 미니맵 비율로 바꾸는 코드
*/
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
    
    //이미지 오브젝트
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
        //가로 세로 Vector3를 Vector2로 저장
        Vector2 mapArea = new Vector2(Vector3.Distance(left.position, right.position), Vector3.Distance(bottom.position, top.position));
        //맵 내에서 캐릭터 위치 계산
        Vector2 charPos = new Vector2(Vector3.Distance(left.position, new Vector3(targetPlayer.transform.position.x, 0f, 0f)),
            Vector3.Distance(bottom.position, new Vector3(0f, targetPlayer.transform.position.y, 0f)));
        Vector2 normalPos = new Vector2(charPos.x / mapArea.x, charPos.y / mapArea.y);
        
        //UI에 표시
        minimapPlayerImage.rectTransform.anchoredPosition = new Vector2(minimapImage.rectTransform.sizeDelta.x * normalPos.x, minimapImage.rectTransform.sizeDelta.y * normalPos.y);
    }

    //오픈 버튼에 들어갈 OnClickEvent
    public void Open()
    {
        gameObject.SetActive(true);
    }
    //닫기 버튼에 들어갈 OnClickEvent
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
