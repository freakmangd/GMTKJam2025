using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wife : MonoBehaviour
{
    private float losTimer;
    private const float losTimerMax = 0.2f;

    public DialogueList[] dialogues;

    enum State
    {
        kitchen,
        check_on_baby,
    }

    private State state = State.kitchen;

    public LayerMask playerLayer;

    public UnityEvent runToBabyStart;

    void Update()
    {
        losTimer -= Time.deltaTime;

        if (state == State.kitchen && losTimer < 0f)
        {
            losTimer = losTimerMax;

            if (Physics.Raycast(transform.position, (PlayerControllerRigidbody.Instance.transform.position - transform.position).normalized, out RaycastHit hit, 20f))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    state = State.check_on_baby;
                    DialogueManager.ins.Speak(dialogues[WhatLoopIsIt.ins.loop - 1].dialogue, runToBabyStart);
                }
            }
        }
    }
}
