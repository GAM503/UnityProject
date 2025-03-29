using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float rotationSpeed = 500f;
    public float animationDampTime = 0.2f;
    public float fallAnimationDampTime = 0.2f;
    public float jumpForce = 10f;

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
        HandleJump();
        MovePlayer();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        if (inputDirection.magnitude > 0)
        {
            moveDirection = mainCamera.transform.rotation * inputDirection;
            targetRotation = Quaternion.LookRotation(moveDirection);
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        animator.SetFloat("moveAmount", moveDirection.magnitude, animationDampTime, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (IsGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalSpeed = jumpForce;
            //animator.SetTrigger("jump");
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
}
