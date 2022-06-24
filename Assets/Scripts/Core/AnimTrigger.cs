using System;
using Interfaces;
using UnityEngine;


public class AnimTrigger : MonoBehaviour
{
    private ITriggerable _triggerable;

    private void Start()
    {
        _triggerable = GetComponentInParent<ITriggerable>();
    }

    public void TriggerAttack()
    {
        _triggerable.OnTriggered();
    }
}