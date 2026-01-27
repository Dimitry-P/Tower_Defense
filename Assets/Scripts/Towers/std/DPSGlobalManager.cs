using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DPSGlobalManager
{
    public static int TotalKillsByDPSTowers;
    public static event Action OnDpsUpgrade;
    public static int CurrentUpgrade { get; private set; }

    public static void RegisterKill()
    {
        TotalKillsByDPSTowers++;

        if (TotalKillsByDPSTowers % 10 == 0)
        {
            CurrentUpgrade++;
            OnDpsUpgrade?.Invoke();
        }
    }
}

