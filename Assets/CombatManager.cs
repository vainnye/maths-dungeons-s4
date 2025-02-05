using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;     // Référence au gestionnaire de santé
    [SerializeField] private Camera mainCamera;              // Référence à la caméra principale
    [SerializeField] private Camera arenaCamera;             // Référence à la caméra de l'arène
    [SerializeField] private Transform arenaPlayerSpawn;     // Point de spawn du joueur dans l'arène
    [SerializeField] private Transform arenaEnemySpawn;      // Point de spawn de l'ennemi dans l'arène
    [SerializeField] private Text questionText;              // Texte pour afficher les questions mathématiques
    [SerializeField] private InputField answerInput;         // Champ pour saisir la réponse
    [SerializeField] private Button submitButton;            // Bouton pour soumettre la réponse

    private bool combatInProgress = false; // Est-ce qu'un combat est en cours ?

    private void Start()
    {
        // Assurez-vous que tout est prêt pour démarrer le combat
    }

    // Démarre le combat et téléporte les personnages dans l'arène
    public void StartCombat(GameObject player, GameObject orc)
    {
        // Téléportation du joueur et de l'ennemi dans l'arène
        player.transform.position = arenaPlayerSpawn.position;
        orc.transform.position = arenaEnemySpawn.position;

        // Désactiver la caméra principale et activer la caméra de l'arène
        mainCamera.enabled = false;
        arenaCamera.enabled = true;

        // Bloquer le mouvement du joueur pendant le combat
        player.GetComponent<PlayerCtrl>().BlockMovement();

        // Démarrer les questions mathématiques
        GenerateQuestion();
    }

    // Génère une nouvelle question mathématique
    private void GenerateQuestion()
    {
        // Exemple de génération d'une question (addition)
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        questionText.text = $"{num1} + {num2} = ?";

        // Enregistrer la bonne réponse (addition dans ce cas)
        int correctAnswer = num1 + num2;

        // Configurer la logique de réponse
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => CheckAnswer(correctAnswer));
    }

    // Vérifie la réponse du joueur
    private void CheckAnswer(int correctAnswer)
    {
        int playerAnswer = int.Parse(answerInput.text);
        if (playerAnswer == correctAnswer)
        {
            // Réponse correcte, infliger des dégâts à l'orc
            healthManager.TakeDamageToOrc(10); // Exemple de dégâts infligés à l'orc
        }
        else
        {
            // Réponse incorrecte, infliger des dégâts au joueur
            healthManager.TakeDamageToPlayer(5); // Exemple de dégâts infligés au joueur
        }

        // Vérifier si le combat est terminé
        if (healthManager.IsOrcDead())
        {
            EndCombat(true); // L'orc est mort
        }
        else if (healthManager.IsPlayerDead())
        {
            EndCombat(false); // Le joueur est mort
        }
        else
        {
            GenerateQuestion(); // Si le combat continue, générer une nouvelle question
        }
    }

    // Fin du combat
    private void EndCombat(bool playerWon)
    {
        // Afficher un message de victoire ou de défaite
        if (playerWon)
        {
            Debug.Log("Le joueur a gagné !");
        }
        else
        {
            Debug.Log("Le joueur a perdu !");
        }

        // Réinitialiser les positions
        // Retour au spawn initial
        // Activer la caméra principale
        mainCamera.enabled = true;
        arenaCamera.enabled = false;

        // Réactiver les mouvements du joueur
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCtrl>().AllowMovement();

        // Réinitialiser la santé des personnages
        healthManager.ResetHealth();
    }
}
