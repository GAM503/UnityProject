using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float threshold = 1f;
    public float rotationSpeed = 500f;
    public float animationDampTime = 0.2f;
    public float verticalSpeed = 0f;
    public bool isGrounded = true;
    public Vector3 groundCheckOffset;
    public float groundCheckRadius;
    public List<LayerMask> groundLayer;
    private Quaternion targetRotation;
    private Camera mainCamera;
    private Animator animator;
    private CharacterController characterController;

    void Awake()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var moveInput = new Vector3(horizontal, 0, vertical).normalized;

        var moveDir = mainCamera.transform.rotation * moveInput;

        checkIsGrounded();
        if (isGrounded)
        {
            verticalSpeed -= 0.5f;
        }
        else
        {
            verticalSpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * speed;
        velocity.y = verticalSpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (moveDir.magnitude > 0)
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        animator.SetFloat("moveAmount", Mathf.Clamp01(moveDir.magnitude), animationDampTime, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void checkIsGrounded()
    {
        foreach (var layer in groundLayer)
        {
            if (Physics.CheckSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius, layer))
            {
                isGrounded = true;
            }
        }
        isGrounded = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.TransformPoint(groundCheckOffset), groundCheckRadius);
    }

}
