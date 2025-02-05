using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;         // Référence à la caméra principale
    [SerializeField] private Camera arenaCamera;        // Référence à la caméra de l'arène
    [SerializeField] private Transform arenaPlayerSpawn; // Point de spawn du joueur dans l'arène
    [SerializeField] private Transform arenaEnemySpawn;  // Point de spawn de l'ennemi dans l'arène
    [SerializeField] private GameObject player;          // Référence au GameObject du joueur

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
        // Vérifier si le joueur entre en collision avec l'ennemi
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Téléportation dans l'arène !");
            StartCombat(collision.gameObject);  // Lancer le combat et la téléportation
        }
    }

    private void StartCombat(GameObject player)
    {
        // Téléportation du joueur et de l'ennemi dans l'arène
        player.transform.position = arenaPlayerSpawn.position;
        transform.position = arenaEnemySpawn.position;

        // Désactiver la caméra principale et activer la caméra de l'arène
        mainCamera.enabled = false;
        arenaCamera.enabled = true;

        // Bloquer le mouvement du joueur pendant le combat
        player.GetComponent<PlayerCtrl>().BlockMovement();
    }

    public void EndCombat(GameObject player)
    {
        // Retourner aux positions initiales après le combat
        player.transform.position = originalPlayerPosition;
        transform.position = originalEnemyPosition;

        // Réactiver la caméra principale et désactiver la caméra de l'arène
        mainCamera.enabled = true;
        arenaCamera.enabled = false;

        // Autoriser à nouveau les mouvements du joueur
        player.GetComponent<PlayerCtrl>().AllowMovement();
    }

}
