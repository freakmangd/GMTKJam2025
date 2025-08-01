using UnityEngine;

public class CerealBoxAnimated : MonoBehaviour
{
    [SerializeField] private GameObject cerealTopper;
    [SerializeField] private GameObject cerealBoxNormal;
    [SerializeField] private ParticleSystem pourSystem;
    [SerializeField] private CerealBowl bowl;

    public void StartPourSystemForPourSimple()
    {
        var main = pourSystem.main;
        main.startLifetime = 0.3f;
        main.startSpeed = 2f;
        pourSystem.Play();
    }

    public void PourSimpleFinish()
    {
        pourSystem.Stop();
    }

    public void EndItAlreadyOhMyGod()
    {
        cerealTopper.SetActive(true);
        cerealBoxNormal.SetActive(true);
        gameObject.SetActive(false);
        PlayerControllerRigidbody.Instance.state = PlayerControllerRigidbody.State.normal;
        bowl.canEat = true;
        bowl.GetComponent<Interactable>().useMessage = "Eat";
    }
}
