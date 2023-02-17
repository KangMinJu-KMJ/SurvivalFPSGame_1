﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject skel_Prefab;
    public List<GameObject> skel_Pool = new List<GameObject>();
   
    [SerializeField]
    private Transform[] skel_Point;
   
    public bool game_Over;
    public int max_Counter = 20;
    public static GameManager single_Gm;

    [Header("soundManager")]
    public float sfx_Volumn = 1.0f;
    public bool sfx_Mute = false;

    private void Awake()
    {
        single_Gm = this;
    }

    void Start()
    {
        game_Over = false;
        skel_Point = GameObject.Find("SkelSpawnPoint").GetComponentsInChildren<Transform>();
        //스켈레톤이 생성될 Transform 초기화

        skel_Prefab = Resources.Load<GameObject>("Skeleton");
        for (int i=0;i<max_Counter;i++)//스켈레톤 풀 생성
        {
            GameObject skel = Instantiate(skel_Prefab);

            skel.SetActive(false);
            skel_Pool.Add(skel);
        }
        StartCoroutine(CreateSkeleton());
    }

    IEnumerator CreateSkeleton()
    {
        while(!game_Over)//게임이 종료되기 전 까지 반복
        {
            yield return new WaitForSeconds(3.0f);//3초마다 풀의 상태를 체크
            if (game_Over) yield break;

            foreach(GameObject skel in skel_Pool)
            {
                if(!skel.activeSelf)//비활성화 된 오브젝트가 있을 시 꺼내준다.
                {
                    int idx = Random.Range(1, skel_Point.Length);
                    skel.transform.position = skel_Point[idx].position;
                    skel.SetActive(true);
                    break;
                }
            }
        }
    }

    public void PlaySfx(Vector3 pos,AudioClip sfx)
    {
        if (sfx_Mute) return;

        GameObject soundObject = new GameObject("sfx");
        soundObject.transform.position = pos;

        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = sfx;
        audioSource.minDistance = 10.0f;
        audioSource.maxDistance = 30.0f;
        audioSource.volume = sfx_Volumn;
        audioSource.Play();
        Destroy(soundObject, sfx.length);

    }
}
