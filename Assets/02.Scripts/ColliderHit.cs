using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHit : MonoBehaviour
{
    public AudioClip hitsound;
    
    [SerializeField]
    private GameObject effect;

    //private void OnCollisionEnter(Collision collision)//통과못하고 충돌감지
    //             //OnTriggerEnter(Collision other) 통과하고 충돌감지
    //{
    //    if(collision.gameObject.tag=="BULLET")
    //    {
    //        Destroy(collision.gameObject);
    //        GameManager.single_Gm.PlaySfx(collision.transform.position, hitsound);

    //        GameObject eff = Instantiate(effect,
    //                                        //Quaternion회전 함수를 만든 수학자
    //            collision.transform.position,Quaternion.identity); //identity정체,멈추다
    //                                        //회전하지말고 생성해라
    //        Destroy(eff, 2.8f);
    //        //스파크 원본 말고 구조체 변수가 2.8초후에 사라진다
    //    }
    //}

    void Awake()
    {
        effect = Resources.Load<GameObject>("Flare");
    }

    void OnDamage(object[] _params)
    {
        Vector3 hitpos = (Vector3)_params[0];
        GameObject copy_Effect = Instantiate(effect, hitpos, Quaternion.identity);
        Destroy(copy_Effect, 2.0f);
    }

}
