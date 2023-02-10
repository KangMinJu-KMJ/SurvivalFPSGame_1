using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    void Start()
    {
        //마우스 커서 활성화
        Cursor.visible = true;
        //Cursor.visible = false; //커서 숨기기
        Cursor.lockState = CursorLockMode.None;
        //마우스 커서를 잠그지 않고 움직이게
        //Cursor.lockState = CursorLockMode.Locked;
        //마우스 커서를 잠그기
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("BattleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    void Update()
    {

    }
}
