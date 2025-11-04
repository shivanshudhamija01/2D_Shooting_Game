using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Vector3 target;
    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        // agent.updateRotation = false;
        // agent.updateUpAxis = false;
    }

    void Update()
    {
        SetTargetPosition();
        SetAgentPosition();

        ForwardDirectionForAnimationSwitching();
    }

    void SetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }

    void ForwardDirectionForAnimationSwitching()
    {
        Debug.Log("Forward vector is : "+ transform.forward);
        float x = transform.forward.x;
        float y = transform.forward.y;

        animator.SetFloat("x",x);
        animator.SetFloat("y",y);
    }
}
