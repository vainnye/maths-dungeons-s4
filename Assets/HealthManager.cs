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

    public void TakeDamageToPlayer(float damage)
    {
        currentPlayerHealth -= damage;
        playerHealthSlider.value = currentPlayerHealth;
    }

    public void TakeDamageToOrc(float damage)
    {
        currentOrcHealth -= damage;
        orcHealthSlider.value = currentOrcHealth;
    }

    public bool IsOrcDead() => currentOrcHealth <= 0;
    public bool IsPlayerDead() => currentPlayerHealth <= 0;

    public void ResetHealth()
    {
        currentPlayerHealth = playerMaxHealth;
        currentOrcHealth = orcMaxHealth;
        playerHealthSlider.maxValue = playerMaxHealth;
        orcHealthSlider.maxValue = orcMaxHealth;
        playerHealthSlider.value = playerMaxHealth;
        orcHealthSlider.value = orcMaxHealth;
    }
}