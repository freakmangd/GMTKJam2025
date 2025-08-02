using UnityEngine;

public class CarKeys : MonoBehaviour
{
    public void Use()
    {
        PlayerControllerRigidbody.Instance.PickupKeys();
        gameObject.SetActive(false);
    }
}
