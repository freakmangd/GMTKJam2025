using UnityEngine;

public class CerealBox : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ParticleSystem pourSystem;
    [SerializeField] private CerealBoxAnimated cerealBoxAnimated;

    void Update()
    {
        if (rb.linearVelocity.sqrMagnitude + rb.angularVelocity.sqrMagnitude > 25 && !pourSystem.isEmitting)
        {
            pourSystem.Play();

        }
        else if (rb.linearVelocity.sqrMagnitude + rb.angularVelocity.sqrMagnitude <= 25 && pourSystem.isEmitting)
        {
            pourSystem.Stop();
        }
    }

    public void OnThrow()
    {
        var main = pourSystem.main;
        main.startLifetime = 5f;
        main.startSpeed = 5f;
    }

    public void PourSimple()
    {
        gameObject.SetActive(false);
        cerealBoxAnimated.gameObject.SetActive(true);
        cerealBoxAnimated.SetTrigger("EasyPour");
    }

    public void PourMinigame()
    {
        gameObject.SetActive(false);
        cerealBoxAnimated.gameObject.SetActive(true);
        cerealBoxAnimated.Loop2StartMinigamePour();
    }
}
