using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float interval = 10.0f;
    [SerializeField] private Transform startingPoint;
    private float _cooldown = 0;

    private void Update()
    {
        _cooldown -= 1 / interval * Time.deltaTime;
        if (_cooldown > 0) return;
        
        var unit = UnitPool.Instance.GetFromPool();
        unit.transform.position = startingPoint.position;
        _cooldown = 1;
    }
}