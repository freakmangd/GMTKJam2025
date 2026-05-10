using UnityEngine;

public class CarKeys : MonoBehaviour
{
    private static string[] loop3Dialogue = new string[] {
        "$Keys",
        "I saw what you did.",
        "That poor cereal. He just wanted to be free, live his life to the fullest.",
        "And you... you stole that from him. What is wrong with you? How could you?",
        "$",
        "Uh, are my keys talking to me now? I really must have slept wrong last night",
    };

    public void Use()
    {
        int loop = WhatLoopIsIt.ins.loop;
        if (loop == 3)
        {
            DialogueManager.ins.Speak(loop3Dialogue, () =>
            {
                PlayerControllerRigidbody.Instance.PickupKeys();
                gameObject.SetActive(false);
            });
            return;
        }

        PlayerControllerRigidbody.Instance.PickupKeys();
        gameObject.SetActive(false);
    }
}
