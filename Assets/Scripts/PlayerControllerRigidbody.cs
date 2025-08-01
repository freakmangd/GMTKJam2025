using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerRigidbody : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed, crouchSpeed, jumpForce, aerialAccel, gravityScale;

    public Camera cam;
    public Vector3 cameraDir { get { return cam.transform.forward; } }
    public Vector3 cameraPos { get { return cam.transform.position; } }

    [SerializeField] private Transform rotator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject model;

    private InputAction movement, look, run, jump, crouch, use, mousepos, throwHeld;

    public static PlayerControllerRigidbody Instance { get { return instance; } }
    private static PlayerControllerRigidbody instance;

    private Vector2 inputDir;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float cameraVerticalClamp;
    private float verticalCamRotation;

    [SerializeField] private Transform groundedPoint;
    [SerializeField] private Vector3 groundedBoxSize;
    [SerializeField] private LayerMask groundLayer;

    private bool grounded;

    [SerializeField] private BoxCollider standingCollider;
    [SerializeField] private BoxCollider crouchingCollider;
    [SerializeField] private Transform standingCamPos;
    [SerializeField] private Transform crouchingCamPos;

    [SerializeField] private float stepHeight;

    Transform currentLadder;
    float ladderAngleRange = 60 / 2;

    public Transform pickupHold;
    [HideInInspector] public CerealBox heldCereal;
    [SerializeField] private ParticleSystem eatSystem;

    [HideInInspector]
    public bool pouredCereal = false, finishedCereal = false, tookOutTrash = false, hasKeys = false;

    private float interactCheckTimer = 0f;
    private const float interactCheckTimerMax = 0.1f;
    [SerializeField] private GameObject usePanel;
    [SerializeField] private TMP_Text useText;

    public enum State
    {
        normal,
        crouching,
        minigame,
    }

    public State state = State.normal;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = InputSystem.actions.FindAction("Move");
        look = InputSystem.actions.FindAction("Look");
        jump = InputSystem.actions.FindAction("Jump");
        crouch = InputSystem.actions.FindAction("Crouch");
        use = InputSystem.actions.FindAction("Interact");
        mousepos = InputSystem.actions.FindAction("MousePos");
        throwHeld = InputSystem.actions.FindAction("Attack");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        bool ignoreInputs = DialogueManager.ins.talking || state == State.minigame;
        Vector2 movementValue = ignoreInputs ? Vector3.zero : movement.ReadValue<Vector2>();
        Vector2 lookValue = ignoreInputs ? Vector3.zero : look.ReadValue<Vector2>();

        float speed = state == State.crouching ? crouchSpeed : moveSpeed;
        inputDir.Set(movementValue.x * speed, movementValue.y * speed);

        float rotX = lookValue.x * mouseSensitivity;
        rotator.Rotate(0, rotX, 0);

        verticalCamRotation -= lookValue.y * mouseSensitivity;
        verticalCamRotation = Mathf.Clamp(verticalCamRotation, -cameraVerticalClamp, cameraVerticalClamp);
        cam.transform.localRotation = Quaternion.Euler(verticalCamRotation, cam.transform.localRotation.y, 0);

        grounded = Physics.CheckBox(groundedPoint.position, groundedBoxSize, Quaternion.identity, groundLayer);

        if (grounded)
        {
            if (jump.IsPressed() && state == State.normal)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            }
        }

        if (crouch.ReadValue<bool>())
        {
            ToggleCrouched(true);
        }

        if (state == State.crouching && !crouch.ReadValue<bool>())
        {
            float checkSize = standingCollider.size.y * 0.95f;

            // Make sure the standing collider wont be intersecting with the ground
            if (!Physics.CheckBox(transform.position + standingCollider.center + (Vector3.up * 0.1f), new Vector3(checkSize, checkSize, checkSize), Quaternion.LookRotation(Vector3.forward), groundLayer))
            {
                ToggleCrouched(false);
            }
        }

        interactCheckTimer -= Time.deltaTime;
        if (interactCheckTimer <= 0f)
        {
            bool didThing = false;
            interactCheckTimer = interactCheckTimerMax;

            if (Physics.Raycast(cam.ScreenPointToRay(mousepos.ReadValue<Vector2>(), Camera.MonoOrStereoscopicEye.Mono), out RaycastHit hit, 2f))
            {
                if (hit.collider.TryGetComponent(out Interactable interactable))
                {
                    if (interactable.enabled)
                    {
                        usePanel.SetActive(true);
                        useText.text = interactable.useMessage;
                        didThing = true;
                    }
                }
            }
            
            if (!didThing)
            {
                usePanel.SetActive(false);
                useText.text = string.Empty;
            }
        }

        if (use.WasPressedThisDynamicUpdate())
        {
            print("try to use");

            if (Physics.Raycast(cam.ScreenPointToRay(mousepos.ReadValue<Vector2>(), Camera.MonoOrStereoscopicEye.Mono), out RaycastHit hit, 2f))
            {
                print("hit something " + hit.collider);

                if (!pouredCereal && heldCereal && hit.collider.CompareTag("Bowl"))
                {
                    print("is bowl? " + hit.collider.gameObject);
                    StartMinigame();
                    heldCereal.PourSimple();
                    pouredCereal = true;
                }
                else if (hit.collider.TryGetComponent(out Interactable interactable))
                {
                    if (interactable.enabled)
                    {
                        print("is interactable? " + interactable);
                        interactable.Interact();
                    }
                }
            }
        }

        if (heldCereal && throwHeld.WasPressedThisDynamicUpdate())
        {
            heldCereal.Throw(cam.transform.forward);
        }

        switch (state)
        {
            case State.normal:
                cam.transform.position = standingCamPos.position;
                break;
            case State.crouching:
                cam.transform.position = crouchingCamPos.position;
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravityScale);

        if (grounded)
            rb.linearVelocity = GroundedSpeed();
        else
            rb.linearVelocity = AerialSpeed();
    }

    Vector3 GroundedSpeed()
    {
        Vector3 speed = new Vector3(inputDir.x, 0, inputDir.y);

        if (currentLadder)
        {
            speed.y = 0;

            Vector2 cam_forward;
            if (cam.transform.forward.y > 0.9f)
                cam_forward = new Vector2(-cam.transform.up.x, -cam.transform.up.z);
            else
                cam_forward = new Vector2(cam.transform.forward.x, cam.transform.forward.z);

            var ladder_forward = new Vector2(currentLadder.forward.x, currentLadder.forward.z);
            float cam_ladder_angle = Vector2.Angle(cam_forward, ladder_forward);

            if (cam_ladder_angle >= 180f - ladderAngleRange && cam_ladder_angle <= 180f + ladderAngleRange)
            {
                speed.x = inputDir.x;
                speed.y = inputDir.y;
                speed.z = inputDir.y > 0 ? speed.z : grounded ? speed.z : 0;
            }
        }

        Vector3 rotatedSpeed = rotator.rotation * speed;
        return new Vector3(rotatedSpeed.x, rb.linearVelocity.y, rotatedSpeed.z);
    }

    void ToggleCrouched(bool toggle)
    {
        if (state != State.normal) return;

        standingCollider.enabled = !toggle;
        crouchingCollider.enabled = toggle;
        state = State.crouching;
    }

    Vector3 AerialSpeed()
    {
        if (currentLadder)
            return GroundedSpeed();

        // Calculate in 2D and then add y vector at the end
        Vector2 vel = new Vector2(rb.linearVelocity.z, rb.linearVelocity.x);
        Vector2 speed = new Vector2(inputDir.y, inputDir.x) * aerialAccel;
        speed = Quaternion.AngleAxis(rotator.eulerAngles.y, Vector3.forward) * speed;
        Vector2 clamped_speed = Vector2.ClampMagnitude(vel + speed * Time.deltaTime, moveSpeed);

        return new Vector3(clamped_speed.y, rb.linearVelocity.y, clamped_speed.x);
    }

    Vector3 scrapped_aerial_speed()
    {
        float neededXVel = Clamp(aerialAccel * Time.fixedDeltaTime * Sign(inputDir.x), 0f, (moveSpeed * Sign(inputDir.x)) - rb.linearVelocity.x);
        float neededZVel = Clamp(aerialAccel * Time.fixedDeltaTime * Sign(inputDir.y), 0f, (moveSpeed * Sign(inputDir.y)) - rb.linearVelocity.z);

        print($"{aerialAccel * Time.fixedDeltaTime * Sign(inputDir.y)}, {0f}, {(moveSpeed * Sign(inputDir.y)) - rb.linearVelocity.z}");

        Vector3 neededSpeed = rotator.rotation * new Vector3(neededXVel, 0, neededZVel);

        Vector3 speed = new Vector3(rb.linearVelocity.x + neededSpeed.x, rb.linearVelocity.y, rb.linearVelocity.z + neededSpeed.z);
        return speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("Ladder"))
        // {
        //     currentLadder = other.transform;
        // }
    }

    private void OnTriggerExit(Collider other)
    {
        // if (other.CompareTag("Ladder"))
        // {
        //     currentLadder = null;
        // }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryStep(collision);
    }

    void TryStep(Collision collision)
    {
        if (!grounded) return;

        ContactPoint contact = collision.GetContact(0);

        // Make sure contact is not from above
        if (Mathf.Round(contact.normal.y) == 0)
        {
            // Find the y pos of where i am touching the ground
            float m_low_y = rb.position.y + standingCollider.center.y - (standingCollider.size.y / 2);

            // Find the y pos of the top of the collider
            Bounds c_bounds = collision.collider.bounds;
            float c_high_y = c_bounds.center.y + c_bounds.extents.y;

            float dist = Mathf.Abs(c_high_y - m_low_y);
            if (dist > 0 && dist <= stepHeight)
            {
                Vector3 vel = collision.relativeVelocity.normalized;

                // Should probably replace 0.1f with the actual distance needed to get onto the other collider
                Vector3 new_pos = rb.position + -vel * 0.1f;

                rb.position = new Vector3(new_pos.x, c_high_y + standingCollider.size.y / 2, new_pos.z);
            }
        }
    }

    public float GetCameraRot()
    {
        return cam.transform.eulerAngles.y;
    }

    public void PickupKeys()
    {
        hasKeys = true;
    }

    public void StartMinigame()
    {
        state = State.minigame;
        model.SetActive(false);
    }

    public void StopMinigame()
    {
        state = State.normal;
        model.SetActive(true);
    }

    public void EatCereal()
    {
        eatSystem.Play();
    }

    float Clamp(float v, float n, float m)
    {
        if (n > m) return Clamp(v, m, n);

        if (v > m)
            return m;
        else if (v < n)
            return n;

        return v;
    }

    float Sign(float v)
    {
        if (v > 0)
            return 1;
        else if (v < 0)
            return -1;
        else
            return 0;
    }
}
