using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Animator))]
public class NPCMovement : MonoBehaviour
{
    private float m_moveSpeed = 0.5f;

    public float moveSpeed{ get { return m_moveSpeed; } set { m_moveSpeed = value; } }

    Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        animator.SetFloat("moveAmount", moveSpeed);
    }


}
