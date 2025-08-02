using UnityEngine;
using UnityEngine.UI;

public class CerealBowl : MonoBehaviour
{
    private int eatCount = 3;
    public bool canEat = false;

    [SerializeField] private GameObject cerealTopper;
    public Transform minigameCamHold;

    [SerializeField] private Slider mashEatSlider;
    private float mashEatPercentage = 0f;

    void Update()
    {
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
                PlayerControllerRigidbody.Instance.EatCereal();
                eatCount -= 1;
            }
            else if (loop == 2)
            {
                PlayerControllerRigidbody.Instance.EatCereal();
                mashEatPercentage += 0.2f;

                if (mashEatPercentage >= 1f)
                {
                    eatCount = 0;
                    mashEatPercentage = 0f;
                }
            }

            if (eatCount == 0)
            {
                cerealTopper.SetActive(false);
                PlayerControllerRigidbody.Instance.finishedCereal = true;
                GetComponent<Interactable>().enabled = false;
            }

            mashEatPercentage -= Time.deltaTime;

            return;
        }

        if (player.heldItem.TryGetComponent(out CerealBox heldCereal))
        {
            if (loop == 1)
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
