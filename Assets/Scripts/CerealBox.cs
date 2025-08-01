using UnityEngine;

public class CerealBox : MonoBehaviour
{
    [SerializeField] private BoxCollider bowlHitbox;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ParticleSystem pourSystem;
    [SerializeField] private Animator cerealBoxAnimated;

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

        var main = pourSystem.main;
        main.startLifetime = 5f;
        main.startSpeed = 5f;

        if (!PlayerControllerRigidbody.Instance.pouredCereal) bowlHitbox.enabled = false;
    }

    public void PourSimple()
    {
        gameObject.SetActive(false);
        cerealBoxAnimated.gameObject.SetActive(true);
        cerealBoxAnimated.SetTrigger("Pour");
    }
}
