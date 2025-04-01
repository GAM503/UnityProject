using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerobj;
    public Rigidbody rb;

    public float rotationSpeed;

    
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb.freezeRotation = true;
    }
    
    
    public void Update()
    {
        //Rotate Orientation
        Vector3 ViewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = ViewDir.normalized;

        //Rotate player object
         float camerahorizontalInput = Input.GetAxis("Horizontal");
         float cameraverticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * cameraverticalInput + orientation.right * camerahorizontalInput;

        if (inputDir != Vector3.zero)
            playerobj.forward = Vector3.Slerp(playerobj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        
    }
    
}
