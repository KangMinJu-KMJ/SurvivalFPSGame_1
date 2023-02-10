using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50.0f;
    public Rigidbody rbody;
    public int damage = 20;
    void Start()
    {                       //velocity=방향 * 속도
        rbody.AddForce(transform.forward * speed);
        Destroy(gameObject, 3.0f);
    }

}
