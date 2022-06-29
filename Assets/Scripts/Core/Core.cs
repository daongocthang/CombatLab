using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> _components = new List<CoreComponent>();

    public void LogicUpdate()
    {
        foreach (var comp in _components)
        {
            comp.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!_components.Contains(component))
        {
            _components.Add(component);
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = _components.OfType<T>().FirstOrDefault();
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