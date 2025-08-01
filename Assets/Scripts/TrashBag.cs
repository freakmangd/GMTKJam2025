using UnityEngine;

public class TrashBag : MonoBehaviour
{
    [SerializeField] private GameObject trashBag;

    public void Empty()
    {
        trashBag.SetActive(false);
        PlayerControllerRigidbody.Instance.tookOutTrash = true;
    }
}
