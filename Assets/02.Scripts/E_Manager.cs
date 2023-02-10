using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Manager : MonoBehaviour
{
    //1. 태어날 위치 정한다.
    //2. 태어날 오브젝트.
    public Transform[] points;
    public GameObject zombiePrefab;
    public GameObject skeletonprefab;
    private float timePrev; //과거시
    private int maxcount = 10;
    private int spawncount = 0;

    void Start()
    {
        //Start에서 현재시를 받았기때문에 timePrev과거시가된다
        timePrev = Time.time; //현재시
    }

    void Update()
    {   //현재시-과거시=지나간시간
        if(Time.time-timePrev>=3.0f)
        {
                    //int강제적 형변환 태어난 좀비의 태그의 숫자를 읽어서 maxcount에 전달
            spawncount =(int) GameObject.FindGameObjectsWithTag("ZOMBIE").Length;
            if (maxcount > spawncount)
            {
                //에너미 생성
                CreateEnemies();
                //과거시에 현재시를 넣어줌으로써 시간을 현재시로 초기화한다
                timePrev = Time.time;
            }
        }
    }

    void CreateEnemies()
    {
                    //SpawnPoint가 0이라 Point 1부터 시작한다 Length배열 길이 자동
        int idx = Random.Range(1, points.Length);
        //1~3사이의 숫자가 랜덤으로 선택
        int i = Random.Range(1, 3);
        //1과 같을때
        if(i==1)
        //랜덤 배열로 생성된 에너미가 포지션에따라 회전한다
        Instantiate(zombiePrefab, points[idx].position,points[idx].rotation);

        //1과 같을때
        else if (i == 2)
        //랜덤 배열로 생성된 에너미가 포지션에따라 회전한다
        Instantiate(skeletonprefab, points[idx].position, points[idx].rotation);

    }
}
