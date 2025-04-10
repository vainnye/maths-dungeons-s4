using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text timerText;

    [Header("Combat management")]
    [SerializeField] private HealthManager healthManager;

    private float timeLimit = 30f;

    
    private string currentCorrectAnswer;
    private float currentTime;

    private GameObject enemyIndungeon;

    private void Awake()
    {
        submitButton.onClick.AddListener( () => CheckAnswer(inputField.text) );
    }


    private void Update()
    {
        if (!GameManager.Instance.CombatInProgress) return;
        
        currentTime -= Time.deltaTime;
        timerText.text = Mathf.Ceil(currentTime) + "s";

        if (Input.GetKeyDown(KeyCode.Return))
            CheckAnswer(inputField.text);

        if (currentTime <= 0)
        {
            Debug.Log("Temps écoulé !");

            // Mort immédiate du joueur
            healthManager.TakeDamageToPlayer(500);

            if (healthManager.IsPlayerDead())
            {
                EndCombat(false);
            }
            else
            {
                NextQuestion(); // On relance la question s'il survit (rare)
            }
        }
    }

    
    public void StartCombat(GameObject enemy)
    {
        enemyIndungeon = enemy;

        switch(GameManager.Instance.Difficulty)
        {
            case 1:
                timeLimit = 30f; // 30 secondes pour la difficulté 2
                break;
            case 2:
                timeLimit = 50f;
                break;
            case 3:
                timeLimit = 100f;
                break;
            case 4:
                timeLimit = 200f;
                break;
        }

        currentTime = timeLimit;

        GameManager.Instance.CombatInProgress = true;

        Debug.Log("combat started");

        NextQuestion();
    }

    public void CheckAnswer(string input)
    {
        if (!Regex.IsMatch(inputField.text, @"\d")) return;

        if (Regex.Replace(inputField.text, @"\s+", "") == Regex.Replace(currentCorrectAnswer, @"\s+", ""))
        {
            healthManager.TakeDamageToOrc(25);
        }
        else
        {
            healthManager.TakeDamageToPlayer(100); // Mauvaise réponse
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
            NextQuestion(); // Question suivante
        }
    }


    private void EndCombat(bool playerWon)
    {
        Debug.Log(playerWon ? "Victoire !" : "Défaite...");

        if (playerWon)
        {
            // Reset vie de l'orc
            healthManager.ResetEnemyHealth();
            GameManager.Instance.Kill(enemyIndungeon); // tuer l'ennemi du donjon
        }

        GameManager.Instance.CombatInProgress = false;
        GameManager.Instance.EndCombat(playerWon);
    }

    private void NextQuestion()
    {
        inputField.text = "";
        (string question, string answer) = QuestionGenerator.GenerateQuestion();
        questionText.text = question;
        currentCorrectAnswer = answer;
    }

}
