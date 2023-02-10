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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < 3; i++)
            {
                Ak47[i].enabled = true;
                M4A1[i].enabled = false;
            }

            spas12.enabled = false;
           
            
            ani.Play("draw");
            ishavem4a1 = false;
            isFire = true; //기본 발사 가능
            idx = 0;
            weaponImage.sprite = weaponIcons[idx];
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < 3; i++)
            {
                Ak47[i].enabled = false;
                M4A1[i].enabled = true;
            }

            spas12.enabled = false;
            
           
            ani.Play("draw");
            ishavem4a1 = false;
            isFire = false; //기본 발사 불가능
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < 3; i++)
            {
                Ak47[i].enabled = false;
                M4A1[i].enabled = false;
            }

            spas12.enabled = true;

            ani.Play("draw");
            ishavem4a1 = true;
            isFire = true; //기본 발사 가능
            idx = 1;
            weaponImage.sprite = weaponIcons[idx];
        }


        
    }

   
}

    