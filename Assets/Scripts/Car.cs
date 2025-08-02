using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Transform cameraHold;
    [SerializeField] private Animator animator;

    public void StartTheFuckinCar()
    {
        PlayerControllerRigidbody player = PlayerControllerRigidbody.Instance;

        if (!player.finishedCereal)
        {
            DialogueManager.ins.Speak(new string[] { "I'm hungry as fuck" }, null);
            return;
        }

        if (!player.tookOutTrash)
        {
            DialogueManager.ins.Speak(new string[] { "Needa take out da traaash" }, null);
            return;
        }

        if (!player.hasKeys)
        {
            DialogueManager.ins.Speak(new string[] { "Oh fuck I forgot my keys" }, null);
            return;
        }

        player.TakeCamera(cameraHold);
        player.StartMinigame();

        animator.SetTrigger("JustDie");
    }
}
