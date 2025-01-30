using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Transform arenaSpawnPoint; // Point de t�l�portation dans l'ar�ne
    [SerializeField] private Transform returnPoint; // Point de retour apr�s le combat
    [SerializeField] private GameObject arenaCamera; // Cam�ra de l'ar�ne
    [SerializeField] private GameObject playerCamera; // Cam�ra principale

    private bool inCombat = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !inCombat) // V�rifie que c'est le joueur
        {
            inCombat = true;

            // T�l�porter le joueur dans l'ar�ne
            other.transform.position = arenaSpawnPoint.position;

            // Activer la cam�ra de l'ar�ne et d�sactiver la cam�ra normale
            arenaCamera.SetActive(true);
            playerCamera.SetActive(false);

            // Lancer le combat (appel d'une m�thode externe si besoin)
            Debug.Log("Combat commenc� !");
        }
    }

    public void EndCombat(GameObject player)
    {
        // T�l�porter le joueur au point de retour
        player.transform.position = returnPoint.position;

        // Activer la cam�ra principale et d�sactiver la cam�ra de l'ar�ne
        arenaCamera.SetActive(false);
        playerCamera.SetActive(true);

        inCombat = false;

        Debug.Log("Combat termin� !");
    }
}


