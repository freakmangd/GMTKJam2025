using UnityEngine;

public class TrashBag : MonoBehaviour
{
    [SerializeField] private GameObject trashBag;

    void OnCollisionEnter(Collision collision)
    {
        print("ok? " + collision.gameObject);

        if (collision.collider.CompareTag("TrashCan"))
        {
            trashBag.SetActive(false);
            PlayerControllerRigidbody.Instance.tookOutTrash = true;
            collision.collider.GetComponent<TrashCan>().ShowTrashBag();
        }
    }
}
