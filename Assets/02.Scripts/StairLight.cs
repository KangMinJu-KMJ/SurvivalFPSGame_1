using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairLight : MonoBehaviour
{
    public Light f_light;
    public AudioClip lightSound; //소리 파일

    public static bool IsExit = false; //라이트 근처를 떠났는지 판단
    public static int lightcount = 0; //OnTriggerEnter)함수 충돌 감지 될때 마다 카운트
    LightObjectPool lightObjectPool; //라이트를 오브젝트 풀링 하는 스크립트 연결

    private void Awake()
    {
        lightObjectPool = GameObject.Find("InLight").GetComponent<LightObjectPool>();        
    }

    private void OnTriggerEnter(Collider other)
    //호출하지 않아도 스스로 호출하는 함수 call back
    //유니티에서 지원 하는 함수 isTrigger 체크시 충돌감지 하는 함수
    {
        if(other.gameObject.tag=="Player"||other.gameObject.tag=="SKELETON")
        {
            lightcount += 1;
            f_light.enabled = true;
            GameManager.single_Gm.PlaySfx(transform.position, lightSound);
            IsExit = false;
            lightObjectPool.LightObjectPoolManager();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            f_light.enabled = false;
            GameManager.single_Gm.PlaySfx(transform.position, lightSound);
            IsExit = true;
        }
    }

}
