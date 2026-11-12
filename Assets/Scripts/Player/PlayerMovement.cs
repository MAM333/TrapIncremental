using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Camera")]
    [SerializeField] Transform cameraHolder;
    [SerializeField] float mouseSensitive = 0.1f;
    [SerializeField] float maxLookAngle = 85f;

    CharacterController characterController;
    PlayerTraps playerTraps;
    Vector2 rawMove;
    Vector2 rawLook;
    float cameraRotationX;
    bool canMove = true;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerTraps = GetComponent<PlayerTraps>();
    }

    public void SetMovementSpeed(float speed)
    {
        moveSpeed = speed;
    }

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        if (!canMove) return;

        HandleLook();
        HandleMovement();
    }

    public void SetRawMove(Vector2 move)
    {
        rawMove = move;
    }

    public void SetRawLook(Vector2 move)
    {
        rawLook = move;
    }

    public void Interact()
    {

    }

    public void UseWeapon(int which)
    {
        playerTraps.UseTrap(which);
    }

    void HandleLook()
    {
        float mouseX = rawLook.x * mouseSensitive;
        float mouseY = rawLook.y * mouseSensitive;

        transform.Rotate(Vector3.up * mouseX);

        cameraRotationX -= mouseY;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -maxLookAngle, maxLookAngle);

        cameraHolder.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }

    void HandleMovement()
    {
        Vector3 direction = transform.right * rawMove.x + transform.forward * rawMove.y;

        direction = Vector3.ClampMagnitude(direction, 1f);

        Vector3 movement = direction * moveSpeed;

        characterController.Move(movement * Time.deltaTime);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DontMove()
    {
        canMove = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
