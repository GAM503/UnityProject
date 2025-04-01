using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{
    private Vector2 PlayerMouseInput;
    private Vector3 PlayerMovementInput;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Sensitivity;
    private float xRot;

    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MovePlayerCamera();
    }
    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.linearVelocity = new Vector3(moveVector.x, PlayerBody.linearVelocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
    
    
    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivity;
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
    
}
