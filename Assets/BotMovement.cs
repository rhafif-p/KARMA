using UnityEngine;
using UnityEngine.AI;

public class BotMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // States
    public float sightRange;
    public bool playerInsightRange;

    public void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInsightRange) { Patroling(); } // Added missing semicolon
        if (playerInsightRange) { ChasePlayer(); } // Added missing semicolon
    }

    private void Patroling()
    {
        if (!walkPointSet) { SearchWalkPoint(); } // Added missing semicolon

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); // Fixed 'tansform' typo
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
