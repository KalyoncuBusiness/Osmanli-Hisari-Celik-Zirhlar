using KalyoncuBusiness.Utils;
using System;
using UnityEngine;

public class TimeTickManager : MonoBehaviour
{
    public class OnTickEventArgs : EventArgs
    {
        public int tick;
    }

    public static event EventHandler<OnTickEventArgs> OnTick;

    public const float TICK_TIMER_MAX = .05f;
    public const float TICK_PER_SECOND = 20f;

    private int tick;
    private float tickTimer;

    private void Awake()
    {
        tick = 0;
    }

    private void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer -= TICK_TIMER_MAX;
            tick++;
            if (OnTick != null) OnTick(this, new OnTickEventArgs { tick = tick });

            var mousePosition = UtilsClass.GetMouseWorldPosition();
        }
    }
}
