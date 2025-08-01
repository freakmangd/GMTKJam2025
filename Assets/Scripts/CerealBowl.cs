using UnityEngine;

public class CerealBowl : MonoBehaviour
{
    private int eatCount = 3;
    public bool canEat = false;

    [SerializeField] private GameObject cerealTopper;

    public void Eat()
    {
        if (!canEat || eatCount == 0) return;

        PlayerControllerRigidbody.Instance.EatCereal();
        eatCount -= 1;

        if (eatCount == 0)
        {
            cerealTopper.SetActive(false);
            PlayerControllerRigidbody.Instance.finishedCereal = true;
            GetComponent<Interactable>().enabled = false;
        }
    }
}
