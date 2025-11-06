using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    // [SerializeField] private float timeGapBetweenAttacks;
    [SerializeField] private Transform target2;
    private Vector3 target;
    private const string distanceToPlayer = "DistanceToPlayer";
    NavMeshAgent agent;
    // private float animatorParameterX;
    // private float animatorParameterY;
    // private float timeSincePreviousAttack = 0f;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }
    void Start()
    {
        if(target2 == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            target2 = player.transform;
        }
        StartCoroutine(UpdatePathDistance());
    }
    void Update()
    {
        
        SetTargetPosition();
        SetAgentPosition();

        SwitchAnimationBasedOnForwardVector();
        // CalculateDistanceToTarget();

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
        agent.SetDestination(new Vector3(target2.position.x, target2.position.y, transform.position.z));
    }

    Vector3 cachesNormal;
    void SwitchAnimationBasedOnForwardVector()
    {
        Vector3 directionVector = target2.position - transform.position;
        Vector3 normalizedDirectionVector = directionVector.normalized;
        if (cachesNormal == normalizedDirectionVector)
            return;
        cachesNormal = normalizedDirectionVector;
        animator.SetFloat("x",normalizedDirectionVector.x);
        animator.SetFloat("y", normalizedDirectionVector.y);

        // Raycasting should also be done in the direction of motion of enemy and raycast is enable when the distance is less than , and ray can recieve the attack after a little delay of time 
        // Ray is changing is direction accordingly 
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, normalizedDirectionVector, 10f);
        // if (hit.collider != null)
        // {
        //     // If the ray hits something, draw it up to the hit point in green
        //     Debug.DrawRay(transform.position, normalizedDirectionVector * hit.distance, Color.green);
        // }
        // else
        // {
        //     // If the ray doesn't hit anything, draw it to its full length in red
        //     Debug.DrawRay(transform.position, normalizedDirectionVector * 10f, Color.red);
        // }
        
        // Make a clean code, and after that 
        // animatorParameterX = normalizedDirectionVector.x;
        // animatorParameterY = normalizedDirectionVector.y;
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
        if (NavMesh.CalculatePath(agent.transform.position, target2.position, NavMesh.AllAreas, path))
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
            // else 
            // {
            //     animator.SetFloat(distanceToPlayer,1f);
            // }
        }
    }
}


// See instead of playing the death animation , we can play the blast effect for the enemies 
// The task of the movement script is to just follow the player and switch between the walk and run animation 

// Attack are handled in another script which will take the reference of the player health or fire an event to inform the player that attack happen 
