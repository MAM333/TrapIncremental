using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference look;
    [SerializeField] InputActionReference interact;
    [SerializeField] InputActionReference useFirstWeapon;
    [SerializeField] InputActionReference useSecondWeapon;
    [SerializeField] InputActionReference useThirdWeapon;
    [SerializeField] InputActionReference useFourthWeapon;

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

        useFirstWeapon.action.performed += OnUseFirstWeapon;
        useSecondWeapon.action.performed += OnUseSecondWeapon;
        useThirdWeapon.action.performed += OnUseThirdWeapon;
        useFourthWeapon.action.performed += OnUseFourthWeapon;
    }

    private void OnEnable()
    {
        move.action.Enable();
        look.action.Enable();
        interact.action.Enable();
        useFirstWeapon.action.Enable();
        useSecondWeapon.action.Enable();
        useThirdWeapon.action.Enable();
        useFourthWeapon.action.Enable();
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

    void OnUseFirstWeapon(InputAction.CallbackContext ctx)
    {
        playerMovement.UseWeapon(0);
    }

    void OnUseSecondWeapon(InputAction.CallbackContext ctx)
    {
        playerMovement.UseWeapon(1);
    }

    void OnUseThirdWeapon(InputAction.CallbackContext ctx)
    {
        playerMovement.UseWeapon(2);
    }

    void OnUseFourthWeapon(InputAction.CallbackContext ctx)
    {
        playerMovement.UseWeapon(3);
    }


    private void OnDisable()
    {
        move.action.Disable();
        look.action.Disable();
        interact.action.Disable();
        useFirstWeapon.action.Disable();
        useSecondWeapon.action.Disable();
        useThirdWeapon.action.Disable();
        useFourthWeapon.action.Disable();
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

        useFirstWeapon.action.performed -= OnUseFirstWeapon;
        useSecondWeapon.action.performed -= OnUseSecondWeapon;
        useThirdWeapon.action.performed -= OnUseThirdWeapon;
        useFourthWeapon.action.performed -= OnUseFourthWeapon;
    }
}
