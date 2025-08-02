using UnityEngine;
using UnityEngine.SceneManagement;

public class CarCrasher : MonoBehaviour
{
    public void NextLoop()
    {
        int loop = WhatLoopIsIt.ins.loop;

        if (loop == 1)
        {
            SceneManager.LoadScene("Loop2");
            DialogueManager.ins.CutToBlack();
        }
    }
}
