using UnityEngine;
using UnityEngine.Events;

public class OnWakeupDialogue : MonoBehaviour
{
    [SerializeField] private DialogueList[] dialogueList;
    [SerializeField] private UnityEvent onDialogueFinish;
    [SerializeField] private Timer timer;

    void Start()
    {
        MusicMan.ins.PlayBeforeTimer();

        DialogueManager.ins.FadeIn(() =>
        {
            int loop = WhatLoopIsIt.ins.loop;

            if (loop <= dialogueList.Length)
            {
                DialogueManager.ins.Speak(dialogueList[WhatLoopIsIt.ins.loop - 1].dialogue, () =>
                {
                    if (loop != 4)
                    {
                        timer.running = true;
                        timer.gameObject.SetActive(true);
                        onDialogueFinish.Invoke();
                        MusicMan.ins.PlayTimerStart();
                    }
                });
            }
        });
    }
}
