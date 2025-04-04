using UnityEngine;

public class Dashing : MonoBehaviour
{
    public float DashSpeed;
    Rigidbody rb;
    bool isDashing;
    float dashProduct = 20f;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isDashing = true;
    }
    private void DashingAbility()
    {
        rb.AddForce(transform.forward * DashSpeed*dashProduct, ForceMode.Impulse);
        isDashing = false;
    }
    public void FixedUpdate()
    {
        if (isDashing)
            DashingAbility();
    }
}
