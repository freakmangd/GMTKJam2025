using UnityEngine;

public class CerealBox : MonoBehaviour
{
    [SerializeField] private BoxCollider bowlHitbox;
    [SerializeField] private Rigidbody rb;

    public void Pickup()
    {
        PlayerControllerRigidbody.Instance.heldCereal = this;

        rb.isKinematic = true;
        rb.detectCollisions = false;

        transform.parent = PlayerControllerRigidbody.Instance.pickupHold;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        bowlHitbox.enabled = true;
    }

    public void Throw(Vector3 dir)
    {
        transform.parent = null;

        rb.isKinematic = false;
        rb.detectCollisions = true;
        rb.AddForce(dir * 10f, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 2f, ForceMode.Impulse);
    }
}
