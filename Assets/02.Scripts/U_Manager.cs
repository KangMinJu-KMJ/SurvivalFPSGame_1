using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class U_Manager : MonoBehaviour
{
    public Text KillTxt;
    public static int total;
    //UI매니저라는 곳에 다른 클래스가 쉽게 접근 하기 위해서
    //대표 변수를 선언
    public static U_Manager umanager;
    //싱글턴
    public Image minimapPos;
    private float timePrev;
    private bool isPaused = false;
    private void Awake()
    {
        KillTxt = GameObject.Find("Canvas-Hp")
            .transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        umanager = this;
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
    }
    void Start()
    {
        
        KillCount(0);
        isPaused = false;
        timePrev = Time.time;

    }

    void Update()
    {
        if(PlayerHP.IsPlayerDie) //기본이 트루
        {
            GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled=false;
            //하이라키에서 Player태그를 가진 오브젝트를 찾아서 꺼버린다.
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseClick();
        }
        if(Time.time - timePrev > 0.5)
        {
            minimapPos.enabled = !minimapPos.enabled;
            timePrev = Time.time;
        }
    }
    public void KillCount(int killsu)
    {
        //킬숫자를 토탈에 넣는다
        total += killsu;
        KillTxt.text = "Kill :" + total.ToString();
    }
    public void OnPauseClick()
    {
        isPaused = !isPaused;
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;
        GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().enabled = !isPaused;
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        var scripts = playerObj.GetComponentsInChildren<MonoBehaviour>();
        foreach(var script in scripts)
        {
            script.enabled = !isPaused;
        }
    }
}
