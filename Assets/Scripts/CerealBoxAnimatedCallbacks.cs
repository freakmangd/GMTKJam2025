using UnityEngine;

// why unity
public class CerealBoxAnimatedCallbacks : MonoBehaviour
{
    [SerializeField] private CerealBoxAnimated cba;

    public void PlaySystem()
    {
        cba.PlaySystem();
    }

    public void StopSystem()
    {
        cba.StopSystem();
    }

    public void PlaySystemMiss()
    {
        cba.PlaySystemMiss();
    }

    public void StartSimplePour()
    {
        cba.StartSimplePour();
    }

    public void EndItAlreadyOhMyGod()
    {
        cba.EndItAlreadyOhMyGod();
    }

    public void YouFuckedUp()
    {
        cba.YouFuckedUp();
    }
}
