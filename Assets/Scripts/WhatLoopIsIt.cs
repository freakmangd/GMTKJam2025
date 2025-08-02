using UnityEngine;

public class WhatLoopIsIt : MonoBehaviour
{
    public int loop;

    public static WhatLoopIsIt ins;

    void Awake()
    {
        if (loop == 0) Debug.LogError("YOU FORGOT TO SET THE LOOP DUMBASS");
        ins = this;
    }
}
