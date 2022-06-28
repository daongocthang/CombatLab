using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float interval = 10.0f;
    [SerializeField] private int maxUnit = 10;
    [SerializeField] private Transform startingPoint;
    private float _cooldown = 0;
    private int numOfAvailable;

    private void Update()
    {
        if (numOfAvailable >= maxUnit) return;
        if (_cooldown <= 0)
        {
            var unit = UnitPool.Instance.GetFromPool();
            unit.transform.position = startingPoint.position;
            _cooldown = 0;
            numOfAvailable += 1;
        }

        _cooldown -= 1 / interval * Time.deltaTime;
    }
}