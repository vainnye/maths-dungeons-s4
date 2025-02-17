using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera arenaCamera;
    [SerializeField] private Transform arenaPlayerSpawn;
    [SerializeField] private Transform arenaEnemySpawn;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject orc;
    [SerializeField] private HealthManager healthManager;

    [Header("UI Combat")]
    [SerializeField] private Text questionText;  // UI classique
    [SerializeField] private InputField answerInput; // UI classique
    [SerializeField] private Button submitButton;

    //private bool isPlayerTurn = true; // Détermine qui joue
    private int currentCorrectAnswer;

    private Vector3 originalPlayerPosition; // Position de départ du joueur
    private Vector3 originalEnemyPosition;  // Position de départ de l’ennemi

    private void Start()
    {
        // Sauvegarde les positions initiales
        originalPlayerPosition = player.transform.position;
        originalEnemyPosition = orc.transform.position;

        // Ajout du listener sur le bouton
        submitButton.onClick.AddListener(CheckAnswer);
    }

    public void StartCombat()
    {
        // Téléportation du joueur et de l’ennemi dans l’arène
        player.transform.position = arenaPlayerSpawn.position;
        orc.transform.position = arenaEnemySpawn.position;

        // Activer la caméra de combat
        mainCamera.enabled = false;
        arenaCamera.enabled = true;

        // Désactiver les mouvements du joueur
        player.GetComponent<PlayerCtrl>().BlockMovement();

        // Démarrer le combat
        GenerateQuestion();
    }

    private void GenerateQuestion()
    {
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        int signe = Random.Range(1, 3);
        if(signe == 1)
        {
            currentCorrectAnswer = num1 + num2;
            questionText.text = $"Combien fait {num1} + {num2} ?";
            answerInput.text = "";
        }
        else if(signe == 2)
        {
            currentCorrectAnswer = num1 - num2;
            questionText.text = $"Combien fait {num1} - {num2} ?";
            answerInput.text = "";
        }
        else if (signe == 3)
        {
            currentCorrectAnswer = num1 * num2;
            questionText.text = $"Combien fait {num1} x {num2} ?";
            answerInput.text = "";
        }
    }

    private void CheckAnswer()
    {
        int playerAnswer;
        if (int.TryParse(answerInput.text, out playerAnswer))
        {
            if (playerAnswer == currentCorrectAnswer)
            {
                // Bonne réponse → Dégâts à l’orc
                healthManager.TakeDamageToOrc(20);
            }
            else
            {
                // Mauvaise réponse → Dégâts au joueur
                healthManager.TakeDamageToPlayer(10);
            }

            // Vérifier si le combat est terminé
            if (healthManager.IsOrcDead())
            {
                EndCombat(true);
            }
            else if (healthManager.IsPlayerDead())
            {
                EndCombat(false);
            }
            else
            {
                GenerateQuestion();
            }
        }
        else
        {
            Debug.Log("Veuillez entrer un nombre valide.");
        }
    }

    private void EndCombat(bool playerWon)
    {
        Debug.Log(playerWon ? "Victoire !" : "Défaite...");

        // Réactiver la caméra principale
        mainCamera.enabled = true;
        arenaCamera.enabled = false;

        // Renvoyer le joueur à sa position initiale
        player.transform.position = originalPlayerPosition;
        orc.transform.position = originalEnemyPosition;

        // Réactiver les mouvements du joueur
        player.GetComponent<PlayerCtrl>().AllowMovement();

        // Réinitialiser la santé
        healthManager.ResetHealth();
    }
}
