using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference look;
    [SerializeField] InputActionReference interact;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        move.action.performed += OnMove;
        move.action.started += OnMove;
        move.action.canceled += OnMove;

        look.action.performed += OnLook;
        look.action.started += OnLook;
        look.action.canceled += OnLook;

        interact.action.performed += OnInteract;
    }

    private void OnEnable()
    {
        move.action.Enable();
        look.action.Enable();
        interact.action.Enable();
    }

    Vector2 rawMove;
    Vector2 rawLook;
    private void Update()
    {
        playerMovement.SetRawMove(rawMove);
        playerMovement.SetRawLook(rawLook);
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        rawMove = ctx.ReadValue<Vector2>();
    }

    void OnLook(InputAction.CallbackContext ctx)
    {
        rawLook = ctx.ReadValue<Vector2>();
    }

    void OnInteract(InputAction.CallbackContext ctx)
    {
        playerMovement.Interact();
    }



    private void OnDisable()
    {
        move.action.Disable();
        look.action.Disable();
        interact.action.Disable();
    }

    private void OnDestroy()
    {
        move.action.performed -= OnMove;
        move.action.started -= OnMove;
        move.action.canceled -= OnMove;

        look.action.performed -= OnLook;
        look.action.started -= OnLook;
        look.action.canceled -= OnLook;

        interact.action.performed -= OnInteract;
    }
}
