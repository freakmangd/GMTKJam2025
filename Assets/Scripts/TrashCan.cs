using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] private GameObject trashBag;

    public void ShowTrashBag()
    {
        trashBag.SetActive(true);
    }
}
