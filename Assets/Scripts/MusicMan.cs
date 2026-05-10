using UnityEngine;

public class MusicMan : MonoBehaviour
{
    public static MusicMan ins;

    private AudioSource current;
    [SerializeField] private AudioSource beforeTimer, timerStart, scaryCat, dream;

    void Awake()
    {
        ins = this;
    }

    public void StopAll()
    {
        current.Stop();
    }

    public void PlayBeforeTimer()
    {
        PlayThingy(beforeTimer);
    }

    public void PlayTimerStart()
    {
        PlayThingy(timerStart);
    }

    public void PlayScaryCat()
    {
        PlayThingy(scaryCat);
    }

    public void PlayDream()
    {
        PlayThingy(dream);
    }

    private void PlayThingy(AudioSource src)
    {
        if (current != null) current.Stop();
        src.Play();
        current = src;
    }
}
