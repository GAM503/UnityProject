using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NPCMovement))]
public class NPCPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    [SerializeField]
    [Tooltip("Is the NPC active?")]
    private bool m_isActive = false;

    public bool IsActive
    {
        get { return m_isActive; }
        set { 
            m_isActive = value; 
            navMeshAgent.enabled = m_isActive;
            if (m_isActive)
            {
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position.position);
            }
        }
    }

    public enum LoopType
    {
        Once,
        PingPong,
        Repeat
    }

    public LoopType loopType;

    [System.Serializable]
    public class Waypoint
    {
        public Transform position;
        public float waitTime = 0f; // Time to wait at this waypoint
    }

    public Waypoint[] waypoints;
    private int m_CurrentWaypointIndex = 0;
    private bool isReversing = false; // For PingPong behavior

    NPCMovement npcMovement;
    private bool isWaiting = false;

    void Awake()
    {
        npcMovement = GetComponent<NPCMovement>();
    }

    void Start()
    {
        if (m_isActive == false)
        {
            navMeshAgent.enabled = false;
            return;
        }
        navMeshAgent.SetDestination(waypoints[0].position.position);
    }

    void Update()
    {
        if (!m_isActive || isWaiting){
            npcMovement.moveSpeed = 0f; // Ensure the NPC's animation reflects the idle state
            return;
        }
            

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            StartCoroutine(WaitAtWaypoint());
        }

        npcMovement.moveSpeed = (navMeshAgent.velocity.magnitude / navMeshAgent.speed) * 0.5f;
    }

    IEnumerator WaitAtWaypoint()
    {
        // Wait at the current waypoint before moving to the next one
        float waitTime = waypoints[m_CurrentWaypointIndex].waitTime;
        if (waitTime > 0f)
        {
            isWaiting = true;
            yield return new WaitForSeconds(waitTime);
        }

        // Move to the next waypoint
        m_CurrentWaypointIndex = GetTheNextWaypoint();
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position.position);

        isWaiting = false;
    }

    int GetTheNextWaypoint()
    {
        if (loopType == LoopType.Once)
        {
            if (m_CurrentWaypointIndex < waypoints.Length - 1)
            {
                return m_CurrentWaypointIndex + 1;
            }
            else
            {
                IsActive = false; // Deactivate NPC when it reaches the last waypoint
                return m_CurrentWaypointIndex;
            }
        }
        else if (loopType == LoopType.PingPong)
        {
            if (isReversing)
            {
                if (m_CurrentWaypointIndex > 0)
                {
                    return m_CurrentWaypointIndex - 1;
                }
                else
                {
                    isReversing = false;
                    return m_CurrentWaypointIndex + 1;
                }
            }
            else
            {
                if (m_CurrentWaypointIndex < waypoints.Length - 1)
                {
                    return m_CurrentWaypointIndex + 1;
                }
                else
                {
                    isReversing = true;
                    return m_CurrentWaypointIndex - 1;
                }
            }
        }
        else // LoopType.Repeat
        {
            return (m_CurrentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
