using UnityEngine;
using UnityEngine.Events;

public class Carryable : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private UnityEvent throwEvent;

    public void Pickup()
    {
        if (PlayerControllerRigidbody.Instance.heldItem != null) return;

        PlayerControllerRigidbody.Instance.heldItem = this;

        rb.isKinematic = true;
        rb.detectCollisions = false;

        transform.parent = PlayerControllerRigidbody.Instance.pickupHold;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Throw(Vector3 dir)
    {
        if (!gameObject.activeSelf) return;

        transform.parent = null;

        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.AddForce(dir * 10f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);

        throwEvent?.Invoke();

        PlayerControllerRigidbody.Instance.heldItem = null;
    }
}
