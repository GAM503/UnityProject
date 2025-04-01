using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform orientation;
    public float movespeed = 10f; // Hız test için artırıldı
    private float movehorizontalInput;
    private float moveverticalInput;
    private Vector3 moveDirection;
    public Rigidbody rb;

    private void FixedUpdate()
    {
        MyInput();
        MovePlayer();
    }

    private void MyInput()
    {
        movehorizontalInput = Input.GetAxisRaw("Horizontal");
        moveverticalInput = Input.GetAxisRaw("Vertical");

        // Input GELİYOR MU?
        Debug.Log($"[INPUT] Horizontal = {movehorizontalInput}, Vertical = {moveverticalInput}");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * moveverticalInput + orientation.right * movehorizontalInput;

        // HAREKET DİREKSİYONU HESAPLANIYOR MU?
        Debug.Log($"[MOVE DIRECTION] {moveDirection}");

        // Rigidbody linearVelocity GÜNCELLENİYOR MU?
        rb.linearVelocity = new Vector3(moveDirection.x * movespeed, rb.linearVelocity.y, moveDirection.z * movespeed);
        Debug.Log($"[LINEAR VELOCITY] {rb.linearVelocity}");

        // Rigidbody fiziksel özellikleri kontrol
        Debug.Log($"[RIGIDBODY] Mass: {rb.mass}, Drag: {rb.linearDamping}, Angular Drag: {rb.angularDamping}, Use Gravity: {rb.useGravity}");
    }
}
