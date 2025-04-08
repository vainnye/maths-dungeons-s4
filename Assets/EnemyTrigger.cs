using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private CombatManager combatManager;

    private bool combatStarted = false;

    private void Start()
    {
        if (combatManager == null)
        {
            Debug.LogError($"CombatManager n'est pas assign� dans l'inspecteur pour {gameObject.name} !");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (combatManager == null)
        {
            Debug.LogError("CombatManager est null dans OnTriggerEnter2D.");
            return;
        }

        if (!combatStarted && collision.CompareTag("Player"))
        {
            Debug.Log($"Le joueur est entr� dans la zone de {gameObject.name}. D�marrage du combat...");
            combatStarted = true;
            combatManager.StartCombat();
        }
    }
}
