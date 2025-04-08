using UnityEngine;
using TMPro;  // N'oubliez pas d'ajouter ce using pour TextMeshPro

public class CombatManager : MonoBehaviour
{
    [Header("Références générales")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject orcPrefab;

    [Header("UI Combat")]
    [SerializeField] private GameObject combatUI;
    [SerializeField] private TMP_Text questionText;  // Remplacer UnityEngine.UI.Text par TMP_Text
    [SerializeField] private TMP_InputField answerInput;  // Remplacer UnityEngine.UI.InputField par TMP_InputField
    [SerializeField] private UnityEngine.UI.Button submitButton;  // Remplacer UnityEngine.UI.Button par TMP_Button (si nécessaire)

    private Camera mainCamera;  // Référence à la caméra principale (Main Camera)
    private bool combatInProgress = false;  // Pour suivre l'état du combat

    private int currentCorrectAnswer;

    private void Start()
    {
        // Obtenir la caméra principale au début
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("La caméra principale n'a pas été trouvée !");
        }
    }

    public void StartCombat()
    {
        // Trouver les modèles existants sur l'arène
        GameObject playerArenaModel = GameObject.Find("PlayerArena");  // Le modèle du joueur dans l'arène
        GameObject orcArenaModel = GameObject.Find("OrcArena");  // Le modèle de l'orc dans l'arène

        if (playerArenaModel == null || orcArenaModel == null)
        {
            Debug.LogError("Les modèles PlayerArena ou OrcArena sont introuvables !");
            return;
        }

        // Configuration de l'UI
        if (combatUI == null || questionText == null || answerInput == null || submitButton == null)
        {
            Debug.LogError("Une ou plusieurs références UI sont manquantes dans l'inspecteur !");
            return;
        }

        combatUI = GameObject.Find("CombatUI");
        questionText = GameObject.Find("QuestionText").GetComponent<TMP_Text>();
        answerInput = GameObject.Find("AnswerInput").GetComponent<TMP_InputField>();
        submitButton = GameObject.Find("SubmitButton").GetComponent<UnityEngine.UI.Button>();

        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => CheckAnswer());

        // Désactiver la caméra principale
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);  // Désactiver la caméra principale
            Debug.Log("Caméra principale désactivée.");
        }
        else
        {
            Debug.LogError("Caméra principale non trouvée !");
            return;
        }

        // Bloquer les déplacements du joueur pendant le combat
        PlayerCtrl ctrl = playerArenaModel.GetComponent<PlayerCtrl>();
        if (ctrl != null)
        {
            ctrl.BlockMovement();
            Debug.Log("Mouvement du joueur bloqué pour le combat.");
        }
        else
        {
            Debug.LogWarning("PlayerCtrl non trouvé sur PlayerArena !");
        }

        // Afficher l'UI du combat
        combatUI.SetActive(true);
        answerInput.interactable = true;
        submitButton.interactable = true;
        answerInput.text = "";
        answerInput.Select();
        answerInput.ActivateInputField();

        // Générer une nouvelle question
        GenerateQuestion();

        // Indiquer qu'un combat a commencé
        combatInProgress = true;
    }

    private void GenerateQuestion()
    {
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        int operation = Random.Range(1, 4);

        if (operation == 1)
        {
            currentCorrectAnswer = num1 + num2;
            questionText.text = $"Combien fait {num1} + {num2} ?";
        }
        else if (operation == 2)
        {
            currentCorrectAnswer = num1 - num2;
            questionText.text = $"Combien fait {num1} - {num2} ?";
        }
        else
        {
            currentCorrectAnswer = num1 * num2;
            questionText.text = $"Combien fait {num1} x {num2} ?";
        }

        answerInput.text = "";

        Debug.Log($"Nouvelle question générée : {questionText.text} (Réponse : {currentCorrectAnswer})");
    }

    private void CheckAnswer()
    {
        HealthManager healthManager = FindObjectOfType<HealthManager>();

        if (int.TryParse(answerInput.text, out int playerAnswer))
        {
            if (playerAnswer == currentCorrectAnswer)
            {
                healthManager.TakeDamageToOrc(20);
            }
            else
            {
                healthManager.TakeDamageToPlayer(10);
            }

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
    }

    private void EndCombat(bool playerWon)
    {
        Debug.Log(playerWon ? "Victoire !" : "Défaite...");

        combatUI.SetActive(false);

        // Réinitialisation de l'UI ou autres actions nécessaires
        // Pas de réinitialisation nécessaire dans ce code

        // Réactiver la caméra principale à la fin du combat
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);  // Réactiver la caméra principale
            Debug.Log("Caméra principale réactivée après le combat.");
        }
    }
}
