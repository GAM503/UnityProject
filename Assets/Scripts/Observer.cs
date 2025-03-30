using UnityEngine;

public class Observer : MonoBehaviour
{
    public GameManager gameManager;
    bool m_IsPlayerInRange;

    public Transform player;

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_IsPlayerInRange = false;
            gameManager.UpdateTheText("Unvisible", Color.white);
        }
    }

    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.parent.position + Vector3.up;
            Ray ray = new Ray(transform.parent.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameManager.UpdateTheText("Visible", Color.red);
                }
                else{
                    gameManager.UpdateTheText("Unvisible", Color.white);
                }
            }

            Debug.DrawRay (ray.origin, ray.direction * 10, Color.red);
        }
    }
}
