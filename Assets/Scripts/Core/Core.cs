using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> components = new List<CoreComponent>();

    public void LogicUpdate()
    {
        foreach (var comp in components)
        {
            comp.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!components.Contains(component))
        {
            components.Add(component);
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = components.OfType<T>().FirstOrDefault();
        if (comp == null)
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        }

        return comp;
    }

    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetCoreComponent<T>();
        return value;
    }
}