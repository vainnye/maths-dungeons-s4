using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;         // R�f�rence � la cam�ra principale
    [SerializeField] private Camera arenaCamera;        // R�f�rence � la cam�ra de l'ar�ne
    [SerializeField] private Transform arenaPlayerSpawn; // Point de spawn du joueur dans l'ar�ne
    [SerializeField] private Transform arenaEnemySpawn;  // Point de spawn de l'ennemi dans l'ar�ne
    [SerializeField] private GameObject player;          // R�f�rence au GameObject du joueur

    private Vector3 originalPlayerPosition; // Position initiale du joueur
    private Vector3 originalEnemyPosition;  // Position initiale de l'ennemi

    private void Start()
    {
        // Sauvegarder les positions initiales du joueur et de l'ennemi
        originalPlayerPosition = player.transform.position;
        originalEnemyPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // V�rifier si le joueur entre en collision avec l'ennemi
        if (collision.CompareTag("Player"))
        {
            Debug.Log("T�l�portation dans l'ar�ne !");
            StartCombat(collision.gameObject);  // Lancer le combat et la t�l�portation
        }
    }

    private void StartCombat(GameObject player)
    {
        // T�l�portation du joueur et de l'ennemi dans l'ar�ne
        player.transform.position = arenaPlayerSpawn.position;
        transform.position = arenaEnemySpawn.position;

        // D�sactiver la cam�ra principale et activer la cam�ra de l'ar�ne
        mainCamera.enabled = false;
        arenaCamera.enabled = true;

        // Bloquer le mouvement du joueur pendant le combat
        player.GetComponent<PlayerCtrl>().BlockMovement();
    }

    public void EndCombat(GameObject player)
    {
        // Retourner aux positions initiales apr�s le combat
        player.transform.position = originalPlayerPosition;
        transform.position = originalEnemyPosition;

        // R�activer la cam�ra principale et d�sactiver la cam�ra de l'ar�ne
        mainCamera.enabled = true;
        arenaCamera.enabled = false;

        // Autoriser � nouveau les mouvements du joueur
        player.GetComponent<PlayerCtrl>().AllowMovement();
    }

}
