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
    Vector2 rawMove;
    Vector2 rawLook;
    float cameraRotationX;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
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
}
