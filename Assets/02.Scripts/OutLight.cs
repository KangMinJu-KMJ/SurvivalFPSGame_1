using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLight : MonoBehaviour
{
    public Light green;
    public Light red;
    public Light blue;

    void Start()
    {
        TurnOn();
    }
    void TurnOn()
    {
        //C# 리턴을 여러번할수있는 함수StartCoroutine 호출한다
        StartCoroutine(LightChange());
    }
    IEnumerator LightChange()
    {
        green.enabled = true;
        red.enabled = false;
        blue.enabled = false;

        //다음 리턴을 3초후에 넘긴다
        yield return new WaitForSeconds(3.0f);
        green.enabled = false;
        red.enabled = true;
        blue.enabled = false;

        yield return new WaitForSeconds(3.0f);
        green.enabled = false;
        red.enabled = false;
        blue.enabled = true;

        yield return new WaitForSeconds(3.0f);
        TurnOn();
    }
}
