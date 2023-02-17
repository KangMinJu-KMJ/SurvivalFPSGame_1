using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hands : MonoBehaviour
{
    //자료형 첫글자만 대문자
    //변수명 전부 소문자
    //상수 전부 대문자 구분이 쉽게 약속해놓자.
    public Animation ani;
    public SkinnedMeshRenderer spas12;
    public MeshRenderer[] Ak47;
    public MeshRenderer[] M4A1;
    public static bool ishavem4a1 = false;
    public static bool isrun = false;
    public static bool isReload = false; //장전불값
    public static bool isFire = false; //기본 발사불값
    public Sprite[] weaponIcons;
    public Image weaponImage;
    public int idx = 0;
    public static int bullets;
    void Start()
    {

    }

    void Update()
    {
        HandAni();
        WeaponChange();
        
    }
    void HandAni()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            ani.Play("runStart");
            //Invoke("WaitSceondScene", 10.0f);
            ani.Play("running");
            isrun = true;
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ani.Play("runStop");
            isrun = false;
        }
    }

    void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //키보드의 숫자패드 1을 눌렀을 시
        {
            for (int i = 0; i < 3; i++)//Ak47의 오브젝트를 활성화, M4A1을 비활성화 한다.
            {
                Ak47[i].enabled = true;
                M4A1[i].enabled = false;
            }
            spas12.enabled = false;
            
            ani.Play("draw");//애니메이션 재생
            ishavem4a1 = false;//M4A1의 상태 변경
            isFire = true; //기본 발사를 참으로 변경
            idx = 0;
            weaponImage.sprite = weaponIcons[idx]; //0번째 배열의 이미지 활성화
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2)) //키보드의 숫자패드 2를 눌렀을 시
        {
            for (int i = 0; i < 3; i++)//마찬가지로 M4A1의 오브젝트 활성화, Ak47 비활성화
            {
                Ak47[i].enabled = false;
                M4A1[i].enabled = true;
            }
            spas12.enabled = false;
            
            ani.Play("draw");//애니메이션 재생
            ishavem4a1 = true;//M4A1의 상태 변경
            isFire = true; //기본 발사를 거짓으로 변경
            idx = 0;
            weaponImage.sprite = weaponIcons[idx];
        }

    }

   
}

    