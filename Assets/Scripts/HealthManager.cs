using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Barres de vie")]
    public Slider playerHealthSlider;
    public Slider enemyHealthSlider;

    [Header("Valeurs de sant�")]
    public const float MAX_PLAYER_HEALTH = 400f;
    public const float MAX_ENEMY_HEALTH = 100f;

    private float currentPlayerHealth;
    private float currentEnemyHealth;

    void Start()
    {
        ResetHealth();
    }

    // M�thode pour infliger des d�g�ts au joueur
    public void TakeDamageToPlayer(float damage)
    {
        Debug.Log("previous player health : " + currentPlayerHealth.ToString());
        if (currentPlayerHealth > 0) // V�rification avant d'infliger des d�g�ts
        {
            currentPlayerHealth -= damage;
            currentPlayerHealth = Mathf.Max(currentPlayerHealth, 0); // S'assurer que la vie ne devienne pas n�gative
            playerHealthSlider.value = currentPlayerHealth;
        }
        Debug.Log($"Le joueur a {currentPlayerHealth} points de vie.");
    }

    // M�thode pour infliger des d�g�ts � l'orc
    public void TakeDamageToOrc(float damage)
    {
        if (currentEnemyHealth > 0) // V�rification avant d'infliger des d�g�ts
        {
            currentEnemyHealth -= damage;
            currentEnemyHealth = Mathf.Max(currentEnemyHealth, 0); // S'assurer que la vie ne devienne pas n�gative
            enemyHealthSlider.value = currentEnemyHealth;
            Debug.Log($"L'orc a {currentEnemyHealth} points de vie.");
        }
    }

    // M�thode pour v�rifier si l'orc est mort
    public bool IsOrcDead() => currentEnemyHealth <= 0;

    // M�thode pour v�rifier si le joueur est mort
    public bool IsPlayerDead() => currentPlayerHealth <= 0;

    // R�initialiser la sant� du joueur et de l'orc
    public void ResetHealth()
    {
        currentPlayerHealth = MAX_PLAYER_HEALTH;
        currentEnemyHealth = MAX_ENEMY_HEALTH;
        playerHealthSlider.maxValue = MAX_PLAYER_HEALTH;
        enemyHealthSlider.maxValue = MAX_ENEMY_HEALTH;
        playerHealthSlider.value = MAX_PLAYER_HEALTH;
        enemyHealthSlider.value = MAX_ENEMY_HEALTH;
        Debug.Log("Sant� r�initialis�e.");
    }

    // M�thode pour r�initialiser uniquement la vie de l'ennemi
    public void ResetEnemyHealth()
    {
        currentEnemyHealth = MAX_ENEMY_HEALTH;
        enemyHealthSlider.value = currentEnemyHealth;
        Debug.Log("La vie de l'ennemi a �t� r�initialis�e � "+MAX_ENEMY_HEALTH);
    }
}
