using System;
using Interfaces;
using UnityEngine;


public class AnimTrigger : MonoBehaviour
{
    public ITriggerable triggerable;

    private void Start()
    {
        triggerable = GetComponentInParent<ITriggerable>();
    }

    public void TriggerAttack()
    {
        triggerable.OnTriggered();
    }
}