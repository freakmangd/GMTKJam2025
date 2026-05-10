using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CerealBowl : MonoBehaviour
{
    private int eatCount = 3;
    public bool canEat = false;

    [SerializeField] private GameObject cerealTopper;
    public Transform minigameCamHold;

    // loop 2
    [SerializeField] private Slider mashEatSlider;
    private float mashEatPercentage = 0f;

    // loop 3
    private bool triedToEat = false;
    [SerializeField] private UnityEvent finishedEatingLoop3;
    [SerializeField] private Rigidbody physxCerealBowl;
    [SerializeField] private Animator anim;

    void Update()
    {
        if (!mashEatSlider) return;

        int loop = WhatLoopIsIt.ins.loop;
        mashEatPercentage = Mathf.Clamp01(mashEatPercentage - Time.deltaTime / 3f);
        mashEatSlider.gameObject.SetActive(mashEatPercentage > 0f);
        mashEatSlider.value = mashEatPercentage;
    }

    public void Use()
    {
        PlayerControllerRigidbody player = PlayerControllerRigidbody.Instance;
        int loop = WhatLoopIsIt.ins.loop;

        if (canEat && eatCount > 0)
        {
            if (loop == 1)
            {
                player.EatCereal();
                eatCount -= 1;
            }
            else if (loop == 2)
            {
                player.EatCereal();
                mashEatPercentage += 0.2f;

                if (mashEatPercentage >= 1f)
                {
                    eatCount = 0;
                    mashEatPercentage = 0f;
                }
            }
            else if (loop == 3 && !triedToEat)
            {
                triedToEat = true;
                player.StartMinigame();
                anim.SetTrigger("RunForLife");

                Util.ins.DoAfterSeconds(2f, () =>
                {
                    player.StopMinigame();
                });
            }
            else if (loop == 3 && triedToEat)
            {
                eatCount = 0;
                finishedEatingLoop3.Invoke();
                gameObject.SetActive(false);
                var r = Instantiate(physxCerealBowl, transform.position, transform.rotation);
                r.AddForce((transform.forward + Vector3.up) * 10f, ForceMode.Impulse);
                r.AddTorque(Random.onUnitSphere * 5f);
                player.EatCereal();

                DialogueManager.ins.Speak(new string[] {
                    "$Bowl of Loops",
                    "Wait, what are you doing? Oh my, STOP, NO! I HAVE FEELINGS TOO! YOU MONSTER!",
                    "$",
                    "The partying last night must not have worn off because I swear my cereal just spoke to me."
                });
            }

            if (eatCount == 0)
            {
                cerealTopper.SetActive(false);
                player.finishedCereal = true;
                GetComponent<Interactable>().enabled = false;
            }

            mashEatPercentage -= Time.deltaTime;

            return;
        }

        if (player.heldItem && player.heldItem.TryGetComponent(out CerealBox heldCereal))
        {
            if (loop == 1 || loop == 3)
            {
                player.StartMinigame();
                heldCereal.PourSimple();
                player.pouredCereal = true;
            }
            else if (loop == 2)
            {
                player.StartMinigame();
                player.TakeCamera(minigameCamHold);
                heldCereal.PourMinigame();
            }
        }
    }
}
