using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float crouchingSpeed = 1.5f;
    public float runningSpeed = 4f;
    public float rotationSpeed = 500f;
    public float animationDampTime = 0.2f;
    public float fallAnimationDampTime = 0.2f;
    public float jumpForce = 10f;
    public float animtaionSensivityFactor = 2f;
    
    private Vector2 moveInput = Vector2.zero;

    private float baseSpeed;
    [Header("Interaction Settings")]
    public float pushForce = 5f;

    [Header("Ground Check Settings")]
    public Vector3 groundCheckOffset;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private float verticalSpeed = 0f;
    private Vector3 moveDirection = Vector3.zero;
    private Quaternion targetRotation;
    private Camera mainCamera;
    private Animator animator;
    private CharacterController characterController;

    private bool IsGrounded
    {
        get => Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    bool hasControl = true;

    void Awake()
    {
        baseSpeed = speed;
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!hasControl)
        {
            return;
        }
        HandleMovement();
        ApplyGravity();
        MovePlayer();
    }

    public void Crouch(CallbackContext context){
        if (context.performed)  // Button pressed
        {
            animator.SetBool("isCrouching", true);
            speed = crouchingSpeed;
        }
        else if (context.canceled)  // Button released
        {
            animator.SetBool("isCrouching", false);
            speed = baseSpeed;
        }
    }

    public void Move(CallbackContext context)
    {
        if (context.performed)  // Button pressed
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else{
            moveInput = Vector2.zero;  // Reset input when button is released
        }
    }

    public void Interact(CallbackContext context)
    {
        if (context.performed)
        {
            print("Interact button pressed");
        }
    }

    public void Run(CallbackContext context)
    {
        if (context.performed)
        {
            speed = runningSpeed;
        }
        else{
            speed = baseSpeed;
        }
    }

    private void HandleMovement()
    {
        float horizontal = moveInput.x;
        float vertical = moveInput.y;

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        if (inputDirection.magnitude > 0)
        {
            // Ignore the camera's X rotation by creating a new rotation with only the Y component
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0; // Remove the X rotation effect
            cameraForward.Normalize();

            Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);
            moveDirection = cameraRotation * inputDirection;
            targetRotation = Quaternion.LookRotation(moveDirection);
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        animator.SetFloat("moveAmount", (moveDirection.magnitude * speed) / ( 2 * baseSpeed), animationDampTime, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void Jump(CallbackContext context)
    {
        if (context.performed && IsGrounded)  // Button pressed and on ground
        {
            print("jumping");
        }
    }

    private void ApplyGravity()
    {
        if (IsGrounded)
        {
            verticalSpeed = -0.5f; // Small downward force to keep grounded
            animator.SetBool("isFalling", false);
            animator.SetFloat("fallAmount", 0, fallAnimationDampTime, Time.deltaTime);
        }
        else
        {
            verticalSpeed += Physics.gravity.y * Time.deltaTime;
            animator.SetBool("isFalling", true);
            animator.SetFloat("fallAmount", 1.2f, fallAnimationDampTime, Time.deltaTime);
        }
    }

    private void MovePlayer()
    {
        Vector3 velocity = moveDirection * speed;
        velocity.y = verticalSpeed;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if(IsGrounded){
            Gizmos.color = Color.white;
        }
        else{
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

    public void SetControl(bool value)
    {
        hasControl = value;
        characterController.enabled = value;

        if(!value)
        {
            animator.SetFloat("moveAmount", 0);
            targetRotation = transform.rotation;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.layer == LayerMask.NameToLayer("Interactable")){
            Rigidbody rb = hit.collider.attachedRigidbody;

            if (rb != null && !rb.isKinematic)
            {
                Vector3 pushDir = hit.moveDirection;
                rb.AddForce(pushDir * pushForce, ForceMode.Force);
            }
        }
        
    }
}
