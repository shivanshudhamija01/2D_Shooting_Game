using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float timeGapBetweenAttacks;
    private Vector3 target;
    private const string distanceToPlayer = "DistanceToPlayer";
    NavMeshAgent agent;
    private float animatorParameterX;
    private float animatorParameterY;
    private float timeSincePreviousAttack = 0f;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Start()
    {
        StartCoroutine(UpdatePathDistance());
    }
    void Update()
    {
        SetTargetPosition();
        SetAgentPosition();

        ForwardDirectionForAnimationSwitching();
        // CalculateDistanceToTarget();
        // Also try to align the raycast in the target direction so that 
    }

    void SetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target = new Vector3(clickPos.x, clickPos.y, 0f);
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }

    void ForwardDirectionForAnimationSwitching()
    {
        Vector3 differenceVector = target - transform.position;
        Vector3 normalizedVector = differenceVector.normalized;
        animator.SetFloat("x",normalizedVector.x);
        animator.SetFloat("y",normalizedVector.y);
        animatorParameterX = normalizedVector.x;
        animatorParameterY = normalizedVector.y;
    }
    IEnumerator UpdatePathDistance()
    {
        while (true)
        {
            CalculateDistanceToTarget();
            yield return new WaitForSeconds(0.2f); 
        }
    }


    void CalculateDistanceToTarget()
    {
        NavMeshPath path = new NavMeshPath();
        // Instead of target use target.position 
        if (NavMesh.CalculatePath(agent.transform.position, target, NavMesh.AllAreas, path))
        {
            float dist = 0f;
            for (int i = 1; i < path.corners.Length; i++)
                dist += Vector2.Distance(path.corners[i - 1], path.corners[i]);
            
            if(dist > 5f)
            {
                animator.SetFloat(distanceToPlayer,0f);
            }
            else if(dist <= 5f && dist > 2f)
            {
                animator.SetFloat(distanceToPlayer,0.5f);
            }
            else 
            {
                animator.SetFloat(distanceToPlayer,1f);
            }
        }
    }
}


