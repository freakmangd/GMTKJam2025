using UnityEngine;

public class BedroomWater : MonoBehaviour
{
    [SerializeField] private Transform growingWater;
    [SerializeField] private GameObject waterSpout;
    public float height = 0f;
    bool sucking = false;

    public bool running = false;
    public static BedroomWater ins;

    void Awake()
    {
        ins = this;
    }

    void Update()
    {
        if (!running) return;

        var scale = growingWater.localScale;
        growingWater.localScale = new Vector3(scale.x, height, scale.z);

        if (sucking)
        {
            height = Mathf.Max(0, height - Time.deltaTime * 3f);
            if (height <= 0f)
            {
                sucking = false;
                gameObject.SetActive(false);
            }
        }
        else
        {
            height = Mathf.Min(7.5f, height + Time.deltaTime);
        }
    }

    public void SuckItUp()
    {
        if (PlayerControllerRigidbody.Instance.heldItem && PlayerControllerRigidbody.Instance.heldItem.CompareTag("Straw"))
        {
            sucking = true;
        }
    }

    public void StartRunning()
    {
        DialogueManager.ins.Speak(new string[] {
            "$Fish",
            "Let us be free!",
            "$",
            "Not again! My fish are trying to get out! Where is my trusty straw?",
        }, () =>
        {
            running = true;
            waterSpout.SetActive(true);
        });
    }
}
