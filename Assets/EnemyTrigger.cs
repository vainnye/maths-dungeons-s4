using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Transform arenaSpawnPoint; // Point de téléportation dans l'arène
    [SerializeField] private Transform returnPoint; // Point de retour après le combat
    [SerializeField] private GameObject arenaCamera; // Caméra de l'arène
    [SerializeField] private GameObject playerCamera; // Caméra principale

    private bool inCombat = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !inCombat) // Vérifie que c'est le joueur
        {
            inCombat = true;

            // Téléporter le joueur dans l'arène
            other.transform.position = arenaSpawnPoint.position;

            // Activer la caméra de l'arène et désactiver la caméra normale
            arenaCamera.SetActive(true);
            playerCamera.SetActive(false);

            // Lancer le combat (appel d'une méthode externe si besoin)
            Debug.Log("Combat commencé !");
        }
    }

    public void EndCombat(GameObject player)
    {
        // Téléporter le joueur au point de retour
        player.transform.position = returnPoint.position;

        // Activer la caméra principale et désactiver la caméra de l'arène
        arenaCamera.SetActive(false);
        playerCamera.SetActive(true);

        inCombat = false;

        Debug.Log("Combat terminé !");
    }
}


