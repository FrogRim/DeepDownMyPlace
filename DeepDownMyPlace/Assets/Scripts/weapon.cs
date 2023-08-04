using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon : MonoBehaviour
{
    public GameObject[] weapons; //무기 오브젝트를 배열로 받음
    public GameObject equipWeapon; //현재 장착 중인 무기 오브젝트
    public int weaponIndex = 0;


    void Start()
    {

    }

    void Update()
    {
        SwitchingWeapon();
    }

    public void SwitchingWeapon()
    {
        

        if (Input.GetKey(KeyCode.Alpha1)) weaponIndex = 0;
        if (Input.GetKey(KeyCode.Alpha2)) weaponIndex = 1;
        if (Input.GetKey(KeyCode.Alpha3)) weaponIndex = 2;

        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3))
        {
            if(weapons[weaponIndex] != null)
            {
                if (equipWeapon != null) equipWeapon.SetActive(false); //이미 무기를 들고 있다면, 장착중이었던 무기 비활성화
                equipWeapon = weapons[weaponIndex]; //선택한 무기로 변경
                equipWeapon.SetActive(true); //활성화
            }
        }
    }
}
