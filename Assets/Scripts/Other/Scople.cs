using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scople : MonoBehaviour
{
    public float rotateSpeed;

    private void OnEnable()
    {
        Quaternion startAngle = Quaternion.Euler(0, 0, 0);
        transform.rotation=startAngle ;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed*Time.deltaTime);
    }
}
