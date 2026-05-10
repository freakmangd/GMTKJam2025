using UnityEngine;
using System;
using System.Collections;

public class Util : MonoBehaviour
{
    public static Util ins;

    void Awake()
    {
        ins = this;
    }

    public void DoAfterSeconds(float seconds, Action action)
    {
        StartCoroutine(DoAfterSecondsCo(seconds, action));
    }

    private IEnumerator DoAfterSecondsCo(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }
}