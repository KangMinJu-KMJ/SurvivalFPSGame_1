using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject Light_1;
    [SerializeField]
    private GameObject Light_2;
    [SerializeField]
    private Transform ParentLightObj;
    public List<GameObject> LightPool = new List<GameObject>();

    void Awake()
    {
        Light_1 = Resources.Load<GameObject>("Stair_Light");
        Light_2 = Resources.Load<GameObject>("Stair_Light");
        ParentLightObj = GetComponent<Transform>();
    }

    void Start()
    {
       for(int i=0;i<1;i++)
        {
            GameObject lightobj1 = (GameObject)Instantiate(Light_1);
            GameObject lightobj2 = (GameObject)Instantiate(Light_2);
            lightobj1.transform.parent = ParentLightObj.transform;
            lightobj2.transform.parent = ParentLightObj.transform;
            lightobj1.transform.localPosition = new Vector3(-3.577f, -0.221f, 25.652f);
            lightobj2.transform.localPosition = new Vector3(-3.577f, -0.221f, 22.222f);
            LightPool.Add(lightobj1);
            LightPool.Add(lightobj2);
        }
    }

    void Update()
    {
        
    }

    public void LightObjectPoolManager()
    {
        if (!StairLight.IsExit)
        {
            if(StairLight.lightcount==1)
            {
                GameObject lightobj2 = (GameObject)Instantiate(Light_2);
                lightobj2.transform.parent = ParentLightObj.transform;
                lightobj2.transform.localPosition = new Vector3(-3.577f, -0.221f, 16.222f);
            }


            if (StairLight.lightcount == 2)
            {
                GameObject lightobj2 = (GameObject)Instantiate(Light_2);
                lightobj2.transform.parent = ParentLightObj.transform;
                lightobj2.transform.localPosition = new Vector3(-3.577f, -0.221f, 13.222f);
            }


            if (StairLight.lightcount == 3)
            {
                GameObject lightobj2 = (GameObject)Instantiate(Light_2);
                lightobj2.transform.parent = ParentLightObj.transform;
                lightobj2.transform.localPosition = new Vector3(-3.577f, -0.221f, 10.222f);
            }


            if (StairLight.lightcount == 4)
            {
                GameObject lightobj2 = (GameObject)Instantiate(Light_2);
                lightobj2.transform.parent = ParentLightObj.transform;
                lightobj2.transform.localPosition = new Vector3(-3.577f, -0.221f, 7.222f);
            }


            if (StairLight.lightcount == 5)
            {
                GameObject lightobj2 = (GameObject)Instantiate(Light_2);
                lightobj2.transform.parent = ParentLightObj.transform;
                lightobj2.transform.localPosition = new Vector3(-3.577f, -0.221f, 4.222f);
            }
        }
    }

   
}
