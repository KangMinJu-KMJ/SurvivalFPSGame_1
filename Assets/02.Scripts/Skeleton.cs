using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//1.컴퍼넌트
//2.추적 거리 공격거리 idle거리
//3.플레이어와 스켈레톤 거리

public enum SkeletonState { idle,trace,attack,die };

public class Skeleton : MonoBehaviour
{
    public SkeletonState MonsterState = SkeletonState.idle;
    [Header("Component")]
    //1.컴퍼넌트 먼저 선언
    //네비 컴퍼넌트
    [SerializeField]
    private NavMeshAgent skel_Navi;
    //애니메이터 컴퍼넌트
    [SerializeField]
    private Animator skel_Ani;
    //플레이어 컴퍼넌트는 하이라키에 저장되어있기때문에 태그로 넣어야한다다
    //플레이어 위치 컴퍼넌트
    [SerializeField]
    private Transform player_Tr;
    //스켈레톤적 위치 컴퍼넌트
    [SerializeField]
    private Transform skel_Tr;
    //스켈레톤적 리지드바디 컴퍼넌트
    [SerializeField]
    private Rigidbody skel_Rbody;

    [Header("Distance")]
    //25안에 있으면 추적을한다
    public float tracedist = 25.0f;
    //3.5안에 있으면 공격한다
    public float attackdist = 3.5f;

    [Header("HPorDMG")]
    //스켈레톤 체력 100
    public int hp = 100;
    //계산에 필요한 hp값
    public int hp_Init;

    //죽었는지 살았는지 불값
    private bool skel_Die;

    [Header("UI")]
    public Image hp_Bar;
    public Canvas hp_Canvas;
    public Text hp_Text;
    [SerializeField]
    private GameObject BloodEffect;

    private void Awake()
    {
        skel_Navi = GetComponent<NavMeshAgent>();
        skel_Ani = GetComponent<Animator>();
        skel_Rbody = GetComponent<Rigidbody>();
        skel_Tr = GetComponent<Transform>();
        player_Tr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //하이라키에 Player태그를 가진 게임 오브젝트안에 트랜스폼을 넘긴다.
        hp_Init=hp;
        skel_Die = false;
        hp_Canvas = gameObject.transform.GetChild(3).GetComponent<Canvas>();
        hp_Bar = gameObject.transform.GetChild(3).transform.GetChild(1).GetComponent<Image>();
        hp_Bar.color = Color.green;
        hp_Text = gameObject.transform.GetChild(3).transform.GetChild(2).GetComponent<Text>();
        BloodEffect = Resources.Load<GameObject>("BloodEffect");
    }

    private void OnEnable()
    {
        StartCoroutine(CheckSkeletonState());
        StartCoroutine(SkeletonAction());
    }

    IEnumerator CheckSkeletonState()
    {
        while(!skel_Die)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(player_Tr.position, skel_Tr.position);
            if(dist<=attackdist)
                MonsterState = SkeletonState.attack;
            else if(dist<=tracedist)
                MonsterState = SkeletonState.trace;
            else
                MonsterState = SkeletonState.idle;
        }
    }

    IEnumerator SkeletonAction()
    {
        while(!skel_Die)
        {
            switch(MonsterState)
            {
                case SkeletonState.idle:
                    skel_Navi.isStopped = true;
                    skel_Ani.SetBool("isTrace",false);
                    break;

                case SkeletonState.trace:
                    skel_Navi.isStopped = false;
                    //skel_Navi.destination = player_Tr.position;
                    skel_Navi.SetDestination(player_Tr.position);
                    skel_Ani.SetBool("isTrace", true);
                    skel_Ani.SetBool("isAttack", false);
                    break;

                case SkeletonState.attack:
                    skel_Navi.isStopped = true;
                    skel_Ani.SetBool("isAttack", true);
                    break;
            }
            yield return null;
        }
    }

    public void OnDamage(object[] _params)
    {
        skel_Ani.SetTrigger("is Hit");
        hp -= (int)_params[1];
        Instantiate(BloodEffect, (Vector3)_params[0], Quaternion.identity);
        
        HpManager();
        
        if(hp<=0)
        SkeletonDie();
    }

    void HpManager()
    {
        hp_Bar.fillAmount = (float)hp / (float)hp_Init;
        if (hp_Bar.fillAmount <= 0.3f)
            hp_Bar.color = Color.red;
        else if (hp_Bar.fillAmount <= 0.5f)
            hp_Bar.color = Color.yellow;
        hp_Text.text = "HP: " + hp.ToString();
    }

    void SkeletonDie()
    {
        StopAllCoroutines();

        skel_Navi.isStopped = true;
        skel_Die = true;
        hp_Canvas.enabled = false;

        GetComponent<CapsuleCollider>().enabled = false;

        skel_Ani.SetTrigger("is Die");


        skel_Rbody.isKinematic = true;
        U_Manager.umanager.KillCount(1);
        foreach (Collider col in GetComponentsInChildren<SphereCollider>())
        {
            col.enabled = false;
        }
        StartCoroutine(PushObjectPool());
    }

    IEnumerator PushObjectPool()
    {
        yield return new WaitForSeconds(3.0f);
        skel_Navi.isStopped = false;
        skel_Die = false;
        hp_Canvas.enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        skel_Rbody.isKinematic = false;
        hp_Bar.fillAmount = 1.0f;
        hp = 100;
        hp_Bar.color = Color.green;
        hp_Text.text = "HP: " + hp.ToString();
        skel_Rbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        skel_Rbody.isKinematic = true; //물리가 사라진다
        foreach (Collider col in GetComponentsInChildren<SphereCollider>())
        {
            col.enabled = true;
        }
        gameObject.SetActive(false);
    }
}
