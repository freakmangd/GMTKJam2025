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

        Transform cam = player.cam.transform;
        cam.parent = cameraHold;
        cam.localPosition = Vector3.zero;
        cam.localRotation = Quaternion.identity;

        player.state = PlayerControllerRigidbody.State.minigame;

        animator.SetTrigger("JustDie");
    }
}
