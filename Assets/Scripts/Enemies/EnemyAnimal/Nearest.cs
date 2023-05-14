using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nearest : MonoBehaviour
{
    public Transform Targrt;
    public float Speed;

    private void Update()
    {
        Vector3 direction = Targrt.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Speed * Time.deltaTime);
    }
}
