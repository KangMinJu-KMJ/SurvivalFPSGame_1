using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirePos : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField]
    public GameObject bulletprefab;
    [SerializeField]
    public Transform fire_pos;

    [Header("Audio")]
    [SerializeField]
    public AudioSource source;
    [SerializeField]
    public AudioClip firesound;

    [Header("Ani")]
    [SerializeField]
    public Animation ani;

    [Header("Magazine")]
    public Image magazineImg;
    public Text magazineTxt;
    public int maxbullet = 10;
    public int remainingbullet = 10;
    public float reloadTime = 2f;
    private bool isReloading = false;

    void Start()
    {
        fire_pos = GetComponent<Transform>();
        firesound = Resources.Load<AudioClip>("gun");
    }

    void Update()
    {
        Debug.DrawRay(fire_pos.position, fire_pos.forward * 20.0f, Color.green);

        if (Hands.isrun == false)
        { 
            if (Input.GetMouseButtonDown(0) && !isReloading)
            {
                if (Hands.isFire == true) //기본발사 => 가능
                {
                    --remainingbullet;
                    Fire();

                    if (Hands.ishavem4a1 == true)//m4a1를 든 상태일 시
                        StartCoroutine(FastShotFire());

                    if (remainingbullet == 0) //총알이 떨어지면 재장전
                        StartCoroutine(Reloading());

                }
            }
        }
    }

    void Fire()
    {
        RaycastHit hit;

        if (Physics.Raycast(fire_pos.position, fire_pos.forward, out hit, 20.0f))
        {
            if (hit.collider.tag == "SKELETON")
            {
                Debug.Log("in Skeleton Tag");
                object[] _params = new object[2];

                _params[0] = hit.point;
                _params[1] = 20;

                hit.collider.gameObject.SendMessage("OnDamage", _params);
            }

            if (hit.collider.tag == "WALL")
            {
                object[] _params = new object[1];
                _params[0] = hit.point;
                hit.collider.gameObject.SendMessage("OnDamage", _params);
            }
        }

        //what무엇을밑에 프리팹 //where      //how rotation 앞에 포지션에 따라
        //Instantiate(bulletprefab, fire_pos.position, fire_pos.rotation);
        // Instantiate 동적 생성 함수 : 오브젝트를 필요 할때 생성 시키는 함수
        source.PlayOneShot(firesound, 1.0f);
        if (Hands.ishavem4a1)
        {
            ani.Play("aimFire");
        }
        else
            ani.Play("fire");

        //재장전 이미지 fillAmount 속성 값을 지정
        magazineImg.fillAmount = (float)remainingbullet / (float)maxbullet;
        UpdateBulletTxt();

    }



    IEnumerator FastShotFire()
    {
        Fire();
        yield return new WaitForSeconds(0.25f);
        Fire();
        yield return new WaitForSeconds(0.25f);
        Fire();
        yield return new WaitForSeconds(0.25f);
    }


    IEnumerator Reloading() //장전 애니메이션
    {
        isReloading = true;
        ani.Play("reloadStart");
        yield return new WaitForSeconds(1.0f);

        ani.Play("reloadCycle");
        yield return new WaitForSeconds(1.0f);

        ani.Play("reloadStop");
        yield return new WaitForSeconds(1.0f);
        isReloading = false; //리로드의 상태 변경과 총알의 UI, 갯수를 다시 채움.
        magazineImg.fillAmount = 1f;
        remainingbullet = maxbullet;
        UpdateBulletTxt(); //남은 총알수 갱신
    }

    void UpdateBulletTxt()
    {
        magazineTxt.text = string.Format("<color=#ff0000>{0}</color>/{1}", remainingbullet, maxbullet);
    }
}