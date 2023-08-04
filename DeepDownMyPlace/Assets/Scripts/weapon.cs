using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon : MonoBehaviour
{
    public GameObject[] weapons; //���� ������Ʈ�� �迭�� ����
    public GameObject equipWeapon; //���� ���� ���� ���� ������Ʈ
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
                if (equipWeapon != null) equipWeapon.SetActive(false); //�̹� ���⸦ ��� �ִٸ�, �������̾��� ���� ��Ȱ��ȭ
                equipWeapon = weapons[weaponIndex]; //������ ����� ����
                equipWeapon.SetActive(true); //Ȱ��ȭ
            }
        }
    }
}
