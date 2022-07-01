using UnityEngine;
using UnityEngine.Events;

public class SandTimer
{
    public delegate void Runnable();

    private Runnable runnable;
    private float delayMillis;
    private float cooldown;

    public SandTimer(Runnable runnable, float delayMillis)
    {
        this.runnable = runnable;
        this.delayMillis = delayMillis;
    }

    public void Start()
    {
        cooldown = 1;
    }

    public bool RunAfterDelayed()
    {
        if (cooldown == 0) return false;
        cooldown -= 1 / delayMillis * Time.deltaTime;
        if (cooldown > 0) return false;
        runnable?.Invoke();
        cooldown = 0;

        return true;
    }
}