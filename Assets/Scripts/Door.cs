using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public float minOpen = -90f;
    public float maxOpen = -220f;

    bool opening;
    float openTimer;

    // loop 3
    bool loop3Minigame = false;
    float loop3OpenPercent = 0f;
    [SerializeField] private GameObject loop3OpenMinigame;
    [SerializeField] private Slider loop3OpenSlider;
    private InputAction kick;
    [SerializeField] private Rigidbody doorPhysx;

    void Start()
    {
        kick = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        if (loop3Minigame)
        {
            if (kick.WasPressedThisDynamicUpdate())
            {
                loop3OpenPercent += 0.25f;
            }
            loop3OpenPercent = Mathf.Clamp01(loop3OpenPercent - Time.deltaTime / 2f);

            loop3OpenSlider.value = loop3OpenPercent;

            if (loop3OpenPercent >= 1f)
            {
                loop3Minigame = false;
                loop3OpenMinigame.SetActive(false);
                PlayerControllerRigidbody.Instance.StopMinigame();
                var rb = Instantiate(doorPhysx, transform.position, transform.rotation);
                rb.AddForce(Vector3.forward * 10f, ForceMode.Impulse);
                rb.AddTorque(Random.onUnitSphere, ForceMode.Impulse);
                gameObject.SetActive(false);
            }

            return;
        }

        if (opening && openTimer <= 1f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Lerp(minOpen, maxOpen, openTimer), transform.eulerAngles.z);
            openTimer += Time.deltaTime;
        }
    }

    public void Open()
    {
        opening = true;
    }

    public void OpenLoop3()
    {
        if (BedroomWater.ins.height > 0f) return;

        PlayerControllerRigidbody.Instance.StartMinigame();
        DialogueManager.ins.Speak(new string[] {
            "Theres no handle... time to kick!"
        }, () =>
        { 
            loop3Minigame = true;
            loop3OpenMinigame.SetActive(true);
        });
    }
}
