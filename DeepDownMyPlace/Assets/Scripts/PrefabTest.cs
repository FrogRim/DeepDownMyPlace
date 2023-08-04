using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
   
    GameObject tank;

    // Start is called before the first frame update
    void Start()
    {
        tank = Managers.Resource.Instantiate("Tank"); // ResourceManager에 있는 생성함수 호출

        /*prefab = Resources.Load<GameObject>("Prefabs/Tank"); 
        tank = Instantiate(prefab); */


        Managers.Resource.Destroy(tank, 3.0f); //ResourceManager에 있는 소멸함수 호출
    }

  
}