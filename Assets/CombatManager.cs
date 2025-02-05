using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;     // R�f�rence au gestionnaire de sant�
    [SerializeField] private Camera mainCamera;              // R�f�rence � la cam�ra principale
    [SerializeField] private Camera arenaCamera;             // R�f�rence � la cam�ra de l'ar�ne
    [SerializeField] private Transform arenaPlayerSpawn;     // Point de spawn du joueur dans l'ar�ne
    [SerializeField] private Transform arenaEnemySpawn;      // Point de spawn de l'ennemi dans l'ar�ne
    [SerializeField] private Text questionText;              // Texte pour afficher les questions math�matiques
    [SerializeField] private InputField answerInput;         // Champ pour saisir la r�ponse
    [SerializeField] private Button submitButton;            // Bouton pour soumettre la r�ponse

    private bool combatInProgress = false; // Est-ce qu'un combat est en cours ?

    private void Start()
    {
        // Assurez-vous que tout est pr�t pour d�marrer le combat
    }

    // D�marre le combat et t�l�porte les personnages dans l'ar�ne
    public void StartCombat(GameObject player, GameObject orc)
    {
        // T�l�portation du joueur et de l'ennemi dans l'ar�ne
        player.transform.position = arenaPlayerSpawn.position;
        orc.transform.position = arenaEnemySpawn.position;

        // D�sactiver la cam�ra principale et activer la cam�ra de l'ar�ne
        mainCamera.enabled = false;
        arenaCamera.enabled = true;

        // Bloquer le mouvement du joueur pendant le combat
        player.GetComponent<PlayerCtrl>().BlockMovement();

        // D�marrer les questions math�matiques
        GenerateQuestion();
    }

    // G�n�re une nouvelle question math�matique
    private void GenerateQuestion()
    {
        // Exemple de g�n�ration d'une question (addition)
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        questionText.text = $"{num1} + {num2} = ?";

        // Enregistrer la bonne r�ponse (addition dans ce cas)
        int correctAnswer = num1 + num2;

        // Configurer la logique de r�ponse
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => CheckAnswer(correctAnswer));
    }

    // V�rifie la r�ponse du joueur
    private void CheckAnswer(int correctAnswer)
    {
        int playerAnswer = int.Parse(answerInput.text);
        if (playerAnswer == correctAnswer)
        {
            // R�ponse correcte, infliger des d�g�ts � l'orc
            healthManager.TakeDamageToOrc(10); // Exemple de d�g�ts inflig�s � l'orc
        }
        else
        {
            // R�ponse incorrecte, infliger des d�g�ts au joueur
            healthManager.TakeDamageToPlayer(5); // Exemple de d�g�ts inflig�s au joueur
        }

        // V�rifier si le combat est termin�
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
            GenerateQuestion(); // Si le combat continue, g�n�rer une nouvelle question
        }
    }

    // Fin du combat
    private void EndCombat(bool playerWon)
    {
        // Afficher un message de victoire ou de d�faite
        if (playerWon)
        {
            Debug.Log("Le joueur a gagn� !");
        }
        else
        {
            Debug.Log("Le joueur a perdu !");
        }

        // R�initialiser les positions
        // Retour au spawn initial
        // Activer la cam�ra principale
        mainCamera.enabled = true;
        arenaCamera.enabled = false;

        // R�activer les mouvements du joueur
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerCtrl>().AllowMovement();

        // R�initialiser la sant� des personnages
        healthManager.ResetHealth();
    }
}
