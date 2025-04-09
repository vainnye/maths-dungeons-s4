using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Barres de vie")]
    public Slider playerHealthSlider;
    public Slider orcHealthSlider;

    [Header("Valeurs de santé")]
    public float playerMaxHealth = 100f;
    public float orcMaxHealth = 100f;

    private float currentPlayerHealth;
    private float currentOrcHealth;

    void Start()
    {
        ResetHealth();
    }

    // Méthode pour infliger des dégâts au joueur
    public void TakeDamageToPlayer(float damage)
    {
        if (currentPlayerHealth > 0) // Vérification avant d'infliger des dégâts
        {
            currentPlayerHealth -= damage;
            currentPlayerHealth = Mathf.Max(currentPlayerHealth, 0); // S'assurer que la vie ne devienne pas négative
            playerHealthSlider.value = currentPlayerHealth;
            Debug.Log($"Le joueur a {currentPlayerHealth} points de vie.");
        }
    }

    // Méthode pour infliger des dégâts à l'orc
    public void TakeDamageToOrc(float damage)
    {
        if (currentOrcHealth > 0) // Vérification avant d'infliger des dégâts
        {
            currentOrcHealth -= damage;
            currentOrcHealth = Mathf.Max(currentOrcHealth, 0); // S'assurer que la vie ne devienne pas négative
            orcHealthSlider.value = currentOrcHealth;
            Debug.Log($"L'orc a {currentOrcHealth} points de vie.");
        }
    }

    // Méthode pour vérifier si l'orc est mort
    public bool IsOrcDead() => currentOrcHealth <= 0;

    // Méthode pour vérifier si le joueur est mort
    public bool IsPlayerDead() => currentPlayerHealth <= 0;

    // Réinitialiser la santé du joueur et de l'orc
    public void ResetHealth()
    {
        currentPlayerHealth = playerMaxHealth;
        currentOrcHealth = orcMaxHealth;
        playerHealthSlider.maxValue = playerMaxHealth;
        orcHealthSlider.maxValue = orcMaxHealth;
        playerHealthSlider.value = playerMaxHealth;
        orcHealthSlider.value = orcMaxHealth;
        Debug.Log("Santé réinitialisée.");
    }

    // Méthode pour réinitialiser uniquement la vie de l'orc
    public void ResetOrcHealth()
    {
        currentOrcHealth = orcMaxHealth;
        orcHealthSlider.value = currentOrcHealth;
        Debug.Log("La vie de l'orc a été réinitialisée à 100.");
    }
}
