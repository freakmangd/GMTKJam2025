using UnityEngine;

public class HouseLayout : MonoBehaviour
{
    [SerializeField] private GameObject garageDoor;

    void Start()
    {
        int loop = WhatLoopIsIt.ins.loop;

        garageDoor.SetActive(loop != 2);
    }
}
