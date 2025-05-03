using UnityEngine;

public class RigidBodyMovement : MonoBehaviour
{

    [SerializeField] private LayerMask Ground;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Transform FeetTransform;
    public float speed = 5f;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Sensitivity;

    private Vector3 moveDirection;

    void Update()
    {
        float xHorizontal = Input.GetAxis("Horizontal");
        float zVertical = Input.GetAxis("Vertical");

        // Doğru yönlerde hareket
        moveDirection = new Vector3(zVertical, 0f, -xHorizontal).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            // Yöne doğru dönüş
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            // Pozisyonu değiştir
            transform.position += moveDirection * speed * Time.deltaTime;
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Physics.CheckSphere(FeetTransform.position, 0.1f, Ground))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }
    }

}
