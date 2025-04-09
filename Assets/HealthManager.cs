using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Barres de vie")]
    public Slider playerHealthSlider;
    public Slider orcHealthSlider;

    [Header("Valeurs de sant�")]
    public float playerMaxHealth = 100f;
    public float orcMaxHealth = 100f;

    private float currentPlayerHealth;
    private float currentOrcHealth;

    void Start()
    {
        ResetHealth();
    }

    // M�thode pour infliger des d�g�ts au joueur
    public void TakeDamageToPlayer(float damage)
    {
        if (currentPlayerHealth > 0) // V�rification avant d'infliger des d�g�ts
        {
            currentPlayerHealth -= damage;
            currentPlayerHealth = Mathf.Max(currentPlayerHealth, 0); // S'assurer que la vie ne devienne pas n�gative
            playerHealthSlider.value = currentPlayerHealth;
            Debug.Log($"Le joueur a {currentPlayerHealth} points de vie.");
        }
    }

    // M�thode pour infliger des d�g�ts � l'orc
    public void TakeDamageToOrc(float damage)
    {
        if (currentOrcHealth > 0) // V�rification avant d'infliger des d�g�ts
        {
            currentOrcHealth -= damage;
            currentOrcHealth = Mathf.Max(currentOrcHealth, 0); // S'assurer que la vie ne devienne pas n�gative
            orcHealthSlider.value = currentOrcHealth;
            Debug.Log($"L'orc a {currentOrcHealth} points de vie.");
        }
    }

    // M�thode pour v�rifier si l'orc est mort
    public bool IsOrcDead() => currentOrcHealth <= 0;

    // M�thode pour v�rifier si le joueur est mort
    public bool IsPlayerDead() => currentPlayerHealth <= 0;

    // R�initialiser la sant� du joueur et de l'orc
    public void ResetHealth()
    {
        currentPlayerHealth = playerMaxHealth;
        currentOrcHealth = orcMaxHealth;
        playerHealthSlider.maxValue = playerMaxHealth;
        orcHealthSlider.maxValue = orcMaxHealth;
        playerHealthSlider.value = playerMaxHealth;
        orcHealthSlider.value = orcMaxHealth;
        Debug.Log("Sant� r�initialis�e.");
    }

    // M�thode pour r�initialiser uniquement la vie de l'orc
    public void ResetOrcHealth()
    {
        currentOrcHealth = orcMaxHealth;
        orcHealthSlider.value = currentOrcHealth;
        Debug.Log("La vie de l'orc a �t� r�initialis�e � 100.");
    }
}
