using System.Linq;
using UnityEngine;

public class Detector : CoreComponent
{
    public bool CheckTargetOutOfRange(GameObject target, float range)
    {
        if (target == null) return true;

        return Vector3.Distance(transform.position, target.transform.position) > range;
    }

    public GameObject FindNearestInWorld(string tagName)
    {
        var hitObjects = GameObject.FindGameObjectsWithTag(tagName);
        if (hitObjects.Length > 0)
        {
            hitObjects = hitObjects.OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
                .ToArray();
            return hitObjects.First();
        }

        return null;
    }

    public GameObject FindNearestInRange(string tagName, float range)
    {
        var objects = GameObject.FindGameObjectsWithTag(tagName);


        objects = objects.Where(e => Vector3.Distance(transform.position, e.transform.position) <= range)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();


        return objects.FirstOrDefault()?.gameObject;
    }
}