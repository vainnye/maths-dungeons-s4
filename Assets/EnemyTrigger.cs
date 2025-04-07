using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private CombatManager combatManager;

    private bool combatStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (combatStarted) return;

        if (collision.CompareTag("Player"))
        {
            combatStarted = true;
            Debug.Log("Début du combat !");
            combatManager.StartCombat();
        }
    }
}

