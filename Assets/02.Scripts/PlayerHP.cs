using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHP : MonoBehaviour
{
    public Image hpbar;
    private int hp = 100;
    private int hpInit = 100;
    private int dmg = 25;
    public static bool IsPlayerDie;
    public Image bloodScreen;
    private WaitForSeconds ws;

    void Start()
    {
        hpbar.color = Color.green;
        IsPlayerDie = false;
        //시작할때 false
        ws = new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="PUNCH")
        {
            HpManager();
        }

        else if(other.gameObject.tag=="SWORD")
        {
            HpManager();
            StartCoroutine(ShowBloodScreen());
        }

        if (hp <= 0)
        {
            //PlayerDie();
        }

    }
    IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1f, 0f, 0f,Random.Range(0.2f,0.3f));
        yield return ws;
        bloodScreen.color = Color.clear;
        //텍스처 색상은 모두 0으로 변경.
    }

    void HpManager()
    {
        hp -= dmg;
        hpbar.fillAmount = (float)hp / (float)hpInit;
        if (hpbar.fillAmount <= 0.3)
            hpbar.color = Color.red;
        else if (hpbar.fillAmount <= 0.5)
            hpbar.color = Color.yellow;
    }

    void PlayerDie()
    {
        IsPlayerDie = true;
        //함수가 호출됬을때 콘솔창에서 확인할수있다
        //Debug.Log("죽었니??");
        //+hp.Tostring()죽었을때 hp까지 출력된다
        //print("Die!!!"+hp.ToString());
        Invoke("WaitSceondScene", 5.0f);
        //Invoke함수를 5초후에 호출한다
    }
    void WaitSceondScene()
    {
        SceneManager.LoadScene("EndScene");
    }
    
}
