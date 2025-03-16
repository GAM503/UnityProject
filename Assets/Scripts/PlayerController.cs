using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    public float threshold = 1f;

    public float rotationSpeed = 500f;
    private Quaternion targetRotation;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var moveInput = new Vector3(horizontal, 0, vertical).normalized;

        var moveDir = mainCamera.transform.rotation * moveInput;

        // Ensure movement stays in the X-Z plane
        moveDir.y = 0;

        if (moveDir.magnitude > 0)
        {
            transform.position += moveDir * speed * Time.deltaTime;
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}
