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
    public float playerMaxHealth = 500f;
    public float orcMaxHealth = 100f;

    private float currentPlayerHealth;
    private float currentOrcHealth;

    void Start()
    {
        currentPlayerHealth = playerMaxHealth;
        currentOrcHealth = orcMaxHealth;

        // Initialisation des barres de vie
        playerHealthSlider.maxValue = playerMaxHealth;
        orcHealthSlider.maxValue = orcMaxHealth;

        UpdateHealthBars();
    }

    // Méthode pour infliger des dégâts au joueur
    public void TakeDamageToPlayer(float damage)
    {
        currentPlayerHealth -= damage;
        if (currentPlayerHealth < 0) currentPlayerHealth = 0;
        UpdateHealthBars();
    }

    // Méthode pour infliger des dégâts à l'orc
    public void TakeDamageToOrc(float damage)
    {
        currentOrcHealth -= damage;
        if (currentOrcHealth < 0) currentOrcHealth = 0;
        UpdateHealthBars();
    }

    // Met à jour les barres de vie
    private void UpdateHealthBars()
    {
        playerHealthSlider.value = currentPlayerHealth;
        orcHealthSlider.value = currentOrcHealth;
    }

    // Retourne si l'orc est mort
    public bool IsOrcDead()
    {
        return currentOrcHealth <= 0;
    }

    // Retourne si le joueur est mort
    public bool IsPlayerDead()
    {
        return currentPlayerHealth <= 0;
    }

    // Réinitialise la santé du joueur et de l'orc
    public void ResetHealth()
    {
        currentPlayerHealth = playerMaxHealth;
        currentOrcHealth = orcMaxHealth;
        UpdateHealthBars();
    }

}
