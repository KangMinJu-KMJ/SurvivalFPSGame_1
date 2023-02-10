using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBloodEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, Random.Range(0.2f, 0.95f));
    }

  
}
