using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private float currentTime;
    public bool running = false;
    private bool exploded = false;

    void Update()
    {
        if (!running) return;

        timerText.gameObject.SetActive(true);

        currentTime = Mathf.Max(currentTime - Time.deltaTime, 0);
        timerText.text = currentTime.ToString("0.00");

        if (currentTime <= 0f && !exploded)
        {
            exploded = true;
            DialogueManager.ins.Speak(new string[] {
                "Well, I guess this is it.",
                "I'm officially late to work.",
                "If I go now, I will be subjected to scalding hot coffee in my facial region. And I would rather blow up.",
                "Goodbye world."
            }, () => DialogueManager.ins.Explosion());
        }
    }
}
