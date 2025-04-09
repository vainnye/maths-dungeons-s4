using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private GameObject orcInDungeon; // Référence à l'orc du donjon

    private bool combatStarted = false;

    // Réinitialise l'état du déclencheur
    public void ResetTrigger()
    {
        combatStarted = false; // Réinitialise l'état du combat pour permettre un nouveau démarrage du combat
    }

    private void Start()
    {
        if (combatManager == null)
        {
            Debug.LogError($"CombatManager n'est pas assigné dans l'inspecteur pour {gameObject.name} !");
        }

        if (orcInDungeon == null)
        {
            Debug.LogError($"L'orc dans le donjon n'est pas assigné pour {gameObject.name} !");
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
            Debug.Log($"Le joueur est entré dans la zone de {gameObject.name}. Démarrage du combat...");
            combatStarted = true;

            // Vérifier si l'orc est assigné avant de démarrer le combat
            if (orcInDungeon != null)
            {
                combatManager.StartCombat(orcInDungeon); // Passer l'orc à la méthode StartCombat
            }
            else
            {
                Debug.LogError("L'orc dans le donjon est null ! Impossible de démarrer le combat.");
            }
        }
    }
}
