using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField]
    private Transform tr;

    [SerializeField]
    private LineRenderer line;

    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        tr = GetComponent<Transform>();
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.enabled = false;

        line.SetWidth(0.3f, 0.01f);
    }

    void Update()
    {
        ray = new Ray(tr.position + Vector3.up * 0.02f, tr.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.blue);

        if(Input.GetMouseButtonDown(0))
        {
            line.SetPosition(0, tr.InverseTransformPoint(ray.origin));

            if(Physics.Raycast(ray,out hit,100))
            {
                line.SetPosition(1, tr.InverseTransformPoint(hit.point));
            }

            else
            {
                line.SetPosition(1, tr.InverseTransformPoint(ray.GetPoint(100.0f)));
            }

            StartCoroutine(ShowLaserBeam());
        }
    }

    IEnumerator ShowLaserBeam()
    {
        line.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
        line.enabled = false;
    }
}
