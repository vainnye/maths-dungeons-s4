using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private GameObject orcInDungeon; // R�f�rence � l'orc du donjon

    private bool combatStarted = false;

    // R�initialise l'�tat du d�clencheur
    public void ResetTrigger()
    {
        combatStarted = false; // R�initialise l'�tat du combat pour permettre un nouveau d�marrage du combat
    }

    private void Start()
    {
        if (combatManager == null)
        {
            Debug.LogError($"CombatManager n'est pas assign� dans l'inspecteur pour {gameObject.name} !");
        }

        if (orcInDungeon == null)
        {
            Debug.LogError($"L'orc dans le donjon n'est pas assign� pour {gameObject.name} !");
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

            // V�rifier si l'orc est assign� avant de d�marrer le combat
            if (orcInDungeon != null)
            {
                combatManager.StartCombat(orcInDungeon); // Passer l'orc � la m�thode StartCombat
            }
            else
            {
                Debug.LogError("L'orc dans le donjon est null ! Impossible de d�marrer le combat.");
            }
        }
    }
}
