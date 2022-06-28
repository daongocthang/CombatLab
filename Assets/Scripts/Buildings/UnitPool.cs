using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UnitPool : MonoBehaviour
{
    [SerializeField] private GameObject unitPrefab;
    public static UnitPool Instance { get; private set; }

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var objToAdd = Instantiate(unitPrefab, transform, true);
            AddToPool(objToAdd);
        }
    }

    private void AddToPool(GameObject obj)
    {
        obj.SetActive(false);
        availableObjects.Enqueue(obj);
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }

        var obj = availableObjects.Dequeue();
        obj.SetActive(true);
        return obj;
    }
}