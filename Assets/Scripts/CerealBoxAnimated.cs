using UnityEngine;
using UnityEngine.InputSystem;

public class CerealBoxAnimated : MonoBehaviour
{
    [SerializeField] private GameObject cerealTopper;
    [SerializeField] private CerealBox cerealBoxNormal;
    [SerializeField] private ParticleSystem pourSystem;
    [SerializeField] private CerealBowl bowl;
    [SerializeField] private Animator anim;

    [SerializeField] private Transform loop2PourSpotLeft;
    [SerializeField] private Transform loop2PourSpotRight;
    bool bobbing = false;
    float loop2BackForthT = 0;
    private InputAction pour;

    void Start()
    {
        pour = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        PlayerControllerRigidbody player = PlayerControllerRigidbody.Instance;
        int loop = WhatLoopIsIt.ins.loop;

        if (loop == 2)
        {
            float sinishT = (Mathf.Sin(loop2BackForthT) + 1f) / 2f;

            if (bobbing)
            {
                loop2BackForthT += Time.deltaTime;
                transform.position = Vector3.Lerp(loop2PourSpotLeft.position, loop2PourSpotRight.position, sinishT);
                print(transform.position);
            }

            if (pour.WasPressedThisDynamicUpdate())
            {
                bobbing = false;

                if (sinishT >= 0.4 && sinishT <= 0.6)
                {
                    anim.SetTrigger("MinigamePour");
                }
                else
                {
                    anim.SetTrigger("BadPour");
                }
            }
        }
    }

    void LateUpdate()
    {
        int loop = WhatLoopIsIt.ins.loop;

        if (loop == 2)
        {
            PlayerControllerRigidbody.Instance.ShowTooltip("Pour");
        }
    }

    public void PlaySystem()
    {
        var main = pourSystem.main;
        main.startLifetime = 0.3f;
        main.startSpeed = 2f;
        pourSystem.Play();
    }

    public void PlaySystemMiss()
    {
        var main = pourSystem.main;
        main.startLifetime = 5f;
        main.startSpeed = 5f;
        pourSystem.Play();
    }

    public void StopSystem()
    {
        pourSystem.Stop();
    }

    public void StartSimplePour()
    {
        // stupid
        transform.position = new Vector3(8.504f, 1.56f, 9.274f);
    }

    public void Loop2StartMinigamePour()
    {
        bobbing = true;
    }

    public void EndItAlreadyOhMyGod()
    {
        cerealTopper.SetActive(true);
        cerealBoxNormal.gameObject.SetActive(true);
        gameObject.SetActive(false);
        PlayerControllerRigidbody.Instance.state = PlayerControllerRigidbody.State.normal;
        PlayerControllerRigidbody.Instance.GiveBackCamera();

        int loop = WhatLoopIsIt.ins.loop;
        bowl.canEat = true;

        if (loop == 1)
        {
            bowl.GetComponent<Interactable>().useMessage = "Eat";
        }
        else if (loop == 2)
        {
            bowl.GetComponent<Interactable>().useMessage = "Eat!!!";
        }
    }

    public void YouFuckedUp()
    {
        PlayerControllerRigidbody player = PlayerControllerRigidbody.Instance;
        print("you fucked it up bud");

        cerealBoxNormal.gameObject.SetActive(true);
        gameObject.SetActive(false);
        player.state = PlayerControllerRigidbody.State.normal;
        player.GiveBackCamera();
        cerealBoxNormal.transform.position = bowl.minigameCamHold.position;
        cerealBoxNormal.GetComponent<Carryable>().Throw(player.cam.transform.forward + (Vector3.up / 2));
    }

    public void SetTrigger(string name)
    {
        anim.SetTrigger(name);
    }
}
