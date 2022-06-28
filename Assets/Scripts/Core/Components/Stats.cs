using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}