using UnityEngine;


public class CombatDummy : MonoBehaviour, IDamageable
{
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}