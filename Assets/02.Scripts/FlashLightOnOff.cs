using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class FlashLightOnOff : MonoBehaviour
{
    public Light flashLight;
    public AudioSource audiosuorce;
    public AudioClip flashSound;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            flashLight.enabled = !flashLight.enabled;
            audiosuorce.PlayOneShot(flashSound, 1.0f);
        }
    }
}
