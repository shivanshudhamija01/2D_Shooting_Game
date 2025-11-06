using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAttack : MonoBehaviour
{
    // Features :  
    // 1. Switch animation to attacking when the player come close or in range , animation and parameter are required   --> 
    // 2. Using raycast for finding the player , when the player come in range, it will fire an event with damage value as each enemy have its own damage value  -->
    // 3. After the player get in range start the countdown , and when the interval is greater than the countdown time , and player health is greater than 100, and is in range
    // 4. Then fire an event with damage value or just take the reference of the player in that script to handle the health 

    public static event Action<float> OnAttack;
    [SerializeField] private Animator animator;
    [SerializeField] private float timeBetweenAttacks = 0.5f;
    [SerializeField] private float damage = 3f;
    [SerializeField] private Transform target2;
    [SerializeField] private float hitDistance=1.3f;
    private Vector3 target;
    private const string distanceToPlayer = "DistanceToPlayer";
    private float timeSincePreviousAttack = 0f;

    bool isPlayerInRange = false;

    void Start()
    {
      if(target2 == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            target2 = player.transform;
        }  
    }
    void Update()
    {
        SearchForThePlayer();

        timeSincePreviousAttack += Time.deltaTime;
        if(timeSincePreviousAttack > timeBetweenAttacks && isPlayerInRange)
        {
            timeSincePreviousAttack = 0;
            // Fire an event
            OnAttack?.Invoke(damage);
        }
    }

    private void SearchForThePlayer()
    {
        Vector3 directionVector = target2.position - transform.position;
        Vector3 normalizedDirectionVector = directionVector.normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, normalizedDirectionVector, hitDistance);
        
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            // If the ray hits something, draw it up to the hit point in green
            Debug.DrawRay(transform.position, normalizedDirectionVector * hit.distance, Color.green);
            // Player in range
            isPlayerInRange = true;
            // Switch the animation to attack animation 
            animator.SetFloat(distanceToPlayer, 1f);
        }
        else
        {
            // If the ray doesn't hit anything, draw it to its full length in red
            Debug.DrawRay(transform.position, normalizedDirectionVector * 10f, Color.red);
            // Player not in the range
            isPlayerInRange = false;
            // Switch the animation to either idle or running 
        }
    }
}
