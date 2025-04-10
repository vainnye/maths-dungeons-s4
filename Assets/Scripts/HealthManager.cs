using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Barres de vie")]
    public Slider playerHealthSlider;
    public Slider enemyHealthSlider;

    [Header("Valeurs de santé")]
    public const float MAX_PLAYER_HEALTH = 400f;
    public const float MAX_ENEMY_HEALTH = 100f;

    private float currentPlayerHealth;
    private float currentEnemyHealth;

    void Start()
    {
        ResetHealth();
    }

    // Méthode pour infliger des dégâts au joueur
    public void TakeDamageToPlayer(float damage)
    {
        Debug.Log("previous player health : " + currentPlayerHealth.ToString());
        if (currentPlayerHealth > 0) // Vérification avant d'infliger des dégâts
        {
            currentPlayerHealth -= damage;
            currentPlayerHealth = Mathf.Max(currentPlayerHealth, 0); // S'assurer que la vie ne devienne pas négative
            playerHealthSlider.value = currentPlayerHealth;
        }
        Debug.Log($"Le joueur a {currentPlayerHealth} points de vie.");
    }

    // Méthode pour infliger des dégâts à l'orc
    public void TakeDamageToOrc(float damage)
    {
        if (currentEnemyHealth > 0) // Vérification avant d'infliger des dégâts
        {
            currentEnemyHealth -= damage;
            currentEnemyHealth = Mathf.Max(currentEnemyHealth, 0); // S'assurer que la vie ne devienne pas négative
            enemyHealthSlider.value = currentEnemyHealth;
            Debug.Log($"L'orc a {currentEnemyHealth} points de vie.");
        }
    }

    // Méthode pour vérifier si l'orc est mort
    public bool IsOrcDead() => currentEnemyHealth <= 0;

    // Méthode pour vérifier si le joueur est mort
    public bool IsPlayerDead() => currentPlayerHealth <= 0;

    // Réinitialiser la santé du joueur et de l'orc
    public void ResetHealth()
    {
        currentPlayerHealth = MAX_PLAYER_HEALTH;
        currentEnemyHealth = MAX_ENEMY_HEALTH;
        playerHealthSlider.maxValue = MAX_PLAYER_HEALTH;
        enemyHealthSlider.maxValue = MAX_ENEMY_HEALTH;
        playerHealthSlider.value = MAX_PLAYER_HEALTH;
        enemyHealthSlider.value = MAX_ENEMY_HEALTH;
        Debug.Log("Santé réinitialisée.");
    }

    // Méthode pour réinitialiser uniquement la vie de l'ennemi
    public void ResetEnemyHealth()
    {
        currentEnemyHealth = MAX_ENEMY_HEALTH;
        enemyHealthSlider.value = currentEnemyHealth;
        Debug.Log("La vie de l'ennemi a été réinitialisée à "+MAX_ENEMY_HEALTH);
    }
}
