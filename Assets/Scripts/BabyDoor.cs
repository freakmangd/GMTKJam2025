using UnityEngine;
using UnityEngine.SceneManagement;

public class BabyDoor : MonoBehaviour
{
    [SerializeField] private Interactable interactable;
    [SerializeField] private GameObject whyWouldItBeTheBaby;

    void Start()
    {
        interactable.enabled = WhatLoopIsIt.ins.loop == 4;
    }

    public void BabyEnding()
    {
        DialogueManager.ins.MakeAChoice(() =>
        {
            DialogueManager.ins.Speak(new string[] {
            "Alright, this is it. The moment of truth. COME ON OUT, YOU MONSTER!",
        }, () =>
        {
            PlayerControllerRigidbody.Instance.StartMinigame();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            whyWouldItBeTheBaby.SetActive(true);
        });
        });
    }

    public void BabyEnding2()
    {
        whyWouldItBeTheBaby.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        DialogueManager.ins.Speak(new string[] {
            "Because, it's a twist? They do it all the time in shows and-",
            "You know what, you're right. Why would it be the baby?",
        }, () =>
        {
            DialogueManager.ins.FadeOut(() =>
            {
                DialogueManager.ins.Speak(new string[] {
                    "What could my baby realistically do? Shoot a pacifier at me?",
                    "I haven’t even technically seen or interacted with him in any of the loops.",
                    "...",
                    "I really should wake up early, and actually see my kid before I leave. I do love my son, afterall. I should show it.",
                }, () =>
                {
                    SceneManager.LoadScene("SampleScene");
                });
            });
        });
    }
}
