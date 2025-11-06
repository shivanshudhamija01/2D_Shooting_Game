using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image healthBar;
    private float playerHealth = 100f;
    void OnEnable()
    {
        AgentAttack.OnAttack += TakeDamage;
    }
    void OnDisable()
    {
        AgentAttack.OnAttack -= TakeDamage;
    }
    
    void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth < 0)
        {
            playerHealth = 0f;
        }
        healthBar.fillAmount = playerHealth/100f;
        Debug.Log("Health of player is : " + playerHealth);
    }
}
