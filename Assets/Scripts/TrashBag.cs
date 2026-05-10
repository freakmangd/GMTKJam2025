using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TrashBag : MonoBehaviour
{
    [SerializeField] private GameObject trashBag;
    [SerializeField] private Carryable carry;

    // Loop 2
    [SerializeField] private Slider fallTimerBar;
    private InputAction catchFallingBag;

    private float nextFallTimer = 0f;
    private const float nextFallTimerMax = 1.5f;

    private float fallTimer = 0f;

    // Loop 3
    private static string[] loop3Dialogue = new string[]
    {
        "$Trash",
        "I know what you are.",
        "$",
        "What?",
        "$Trash",
        "Your sins will never be forgiven. Your lies, your greed, the tragedies that have befallen all who exist within your wrath.",
        "There will come a day. On this day, light will shine down. Many will be uplifted off the harsh scale of life.",
        "Wishes granted, conflicts relinquished. And then there will be you. On the other end of the scale, absorbing all of the world's problems into a singular, unforgiven soul.",
        "You will bear it all. The struggles, the suffering, the pain, the sorrow. And when the clouds ring out their final call... the only invitation you will receive is that of a wingless dove. No freedom. No love. Only hurt.",
        "$",
        "I uh... wow. I... I'm sorry?",
        "I didn't realize I... what is wrong with me? That poor cereal...",
        "Wait.",
        "Does this make me a cereal killer?",
        "Ah, I don't have time to contemplate my existence and how it affects others in the long run on a moral scale, I have to get to work!"
    };
    private bool pickedUp = false;

    void Start()
    {
        catchFallingBag = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        int loop = WhatLoopIsIt.ins.loop;

        if (loop == 2 && carry.isHeld)
        {
            if (fallTimer > 0f)
            {
                fallTimer -= Time.deltaTime / 0.8f;

                Vector3 rot = transform.eulerAngles;
                transform.eulerAngles = new Vector3(Mathf.Sin(Time.time * 10f) * 30f, rot.y, rot.z);

                if (catchFallingBag.WasPressedThisDynamicUpdate())
                {
                    transform.localEulerAngles = Vector3.zero;
                    fallTimer = -1f;
                }
                else if (fallTimer <= 0f)
                {
                    carry.Throw(-PlayerControllerRigidbody.Instance.cam.transform.forward + Vector3.up);
                }
            }
            else
            {
                nextFallTimer += Time.deltaTime;

                if (nextFallTimer >= nextFallTimerMax)
                {
                    nextFallTimer = 0f;
                    fallTimer = 1f;
                }
            }
        }
    }

    void LateUpdate()
    {
        if (fallTimer > 0f && Mathf.FloorToInt(fallTimer * 10f) % 2 == 0)
        {
            PlayerControllerRigidbody.Instance.ShowTooltip("Catch!!!");
        }
    }

    public void OnPickup()
    {
        fallTimer = -1f;
        nextFallTimer = 0f;

        if (!pickedUp && WhatLoopIsIt.ins.loop == 3)
        {
            pickedUp = true;
            DialogueManager.ins.Speak(loop3Dialogue);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("TrashCan"))
        {
            trashBag.SetActive(false);
            PlayerControllerRigidbody.Instance.tookOutTrash = true;
            collision.collider.GetComponent<TrashCan>().ShowTrashBag();
        }
    }
}
