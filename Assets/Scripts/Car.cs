using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    [SerializeField] private Transform cameraHold;
    [SerializeField] private Animator animator;

    private bool startingCar = false;

    [SerializeField] private GameObject carGoVroomPanel;
    [SerializeField] private TMP_Text whatButtonPressText;
    private InputAction[] carGoButtons;
    private const string carGoString = "CARGOVROOM";
    int carGoIndex = 0;

    void Start()
    {
        carGoButtons = new InputAction[] {
            InputSystem.actions.FindAction("C"),
            InputSystem.actions.FindAction("A"),
            InputSystem.actions.FindAction("R"),
            InputSystem.actions.FindAction("G"),
            InputSystem.actions.FindAction("O"),
            InputSystem.actions.FindAction("V"),
            InputSystem.actions.FindAction("R"),
            InputSystem.actions.FindAction("O"),
            InputSystem.actions.FindAction("O"),
            InputSystem.actions.FindAction("M"),
        };
    }

    void Update()
    {
        int loop = WhatLoopIsIt.ins.loop;

        if (loop == 2 && startingCar)
        {
            whatButtonPressText.text = string.Format("Press {0}!", carGoString[carGoIndex]);
            if (carGoIndex == 8)
            {
                whatButtonPressText.text = string.Format("Press {0} AGAIN!", carGoString[carGoIndex]);
            }

            if (carGoButtons[carGoIndex].WasPressedThisDynamicUpdate())
            {
                carGoIndex += 1;
                PlayerControllerRigidbody.Instance.EatCereal();

                if (carGoIndex >= carGoButtons.Length)
                {
                    startingCar = false;
                    carGoVroomPanel.SetActive(false);
                    KillPlayerLol();
                }
            }
        }
    }

    public void StartTheFuckinCar()
    {
        int loop = WhatLoopIsIt.ins.loop;
        PlayerControllerRigidbody player = PlayerControllerRigidbody.Instance;

        if (loop == 4)
        {
            DialogueManager.ins.MakeAChoice(() =>
            {
            DialogueManager.ins.Speak(new string[] {
                "It has to be this guy hitting me, it's the whole reason I wake up in the next loop!",
                "I'll try believing really hard that he won't hit me, and stopping the car just in time.",
                "... this isn't gonna work is it...",
            }, () =>
            {
                PlayerControllerRigidbody.Instance.StartMinigame();
                PlayerControllerRigidbody.Instance.TakeCamera(cameraHold);

                DialogueManager.ins.Speak(new string[] {
                    "Alright, let's do this!"
                }, () =>
                {
                    animator.SetTrigger("CarEnding");
                });
            });
            });
            return;
        }

        if (!player.finishedCereal)
        {
            DialogueManager.ins.Speak(new string[] { "I still need to eat!" });
            return;
        }

        if (!player.tookOutTrash)
        {
            DialogueManager.ins.Speak(new string[] { "Oh yeah, my wife said I have to take out the trash..." });
            return;
        }

        if (!player.hasKeys)
        {
            DialogueManager.ins.Speak(new string[] { "Oh no I forgot my keys" });
            return;
        }

        if (loop == 2 && !player.hasJumperCables)
        {
            DialogueManager.ins.Speak(new string[] { "The battery's dead, I have jumper cables somewhere..." });
            return;
        }

        if (loop == 3 && !player.hasTransmission)
        {
            DialogueManager.ins.Speak(new string[] { "The transmission is missing???", "Someone small must have stolen it..." });
            return;
        }

        if (loop == 1)
        {
            KillPlayerLol();
        }
        else if (loop == 2)
        {
            carGoVroomPanel.SetActive(true);
            player.StartMinigame();
            startingCar = true;
        }
        else if (loop == 3)
        {
            KillPlayerLol();
        }
    }

    public void KillPlayerLol()
    {
        PlayerControllerRigidbody.Instance.StartMinigame();
        PlayerControllerRigidbody.Instance.TakeCamera(cameraHold);
        animator.SetTrigger("JustDie");
    }
}
