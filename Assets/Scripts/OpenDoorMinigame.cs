using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoorMinigame : MonoBehaviour
{
    [SerializeField] private RectTransform hand;
    private InputAction mousePos;

    void Start()
    {
        mousePos = InputSystem.actions.FindAction("MousePos");
    }

    void Update()
    {
        hand.position = mousePos.ReadValue<Vector2>();
    }

    public void StartMinigame()
    {
        Cursor.lockState = CursorLockMode.None;
        // Mouse.current.WarpCursorPosition(new Vector2(620, 169));
    }

    public void EndMinigame()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
