using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
    [SerializeField] private GameObject combatUI;
    [SerializeField] private Text questionText;
    [SerializeField] private InputField answerInput;
    [SerializeField] private Button submitButton;

    private int currentCorrectAnswer;
    private Vector3 originalPlayerPosition;
    private Vector3 originalEnemyPosition;

    private void Start()
    {
        originalPlayerPosition = player.transform.position;
        originalEnemyPosition = orc.transform.position;
        submitButton.onClick.AddListener(CheckAnswer);
        combatUI.SetActive(false);
    }

    public void StartCombat()
    {
        Debug.Log("StartCombat() appelé !");
        Debug.Log("combatUI est actif ? " + combatUI.activeSelf);
        Debug.Log("Bouton interactable ? " + submitButton.interactable);
        Debug.Log("InputField interactable ? " + answerInput.interactable);

        player.transform.position = arenaPlayerSpawn.position;
        orc.transform.position = arenaEnemySpawn.position;

        mainCamera.enabled = false;
        arenaCamera.enabled = true;

        player.GetComponent<PlayerCtrl>().BlockMovement();
        combatUI.SetActive(true);
        Debug.Log("combatUI activé après SetActive : " + combatUI.activeSelf);
        answerInput.interactable = true;
        submitButton.interactable = true;  // Active le bouton
        answerInput.text = "";
        answerInput.Select();  // Focus automatique sur l'input
        answerInput.ActivateInputField();
        GenerateQuestion();
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
    }

    private void CheckAnswer()
    {
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

        mainCamera.enabled = true;
        arenaCamera.enabled = false;

        player.transform.position = originalPlayerPosition;
        orc.transform.position = originalEnemyPosition;

        player.GetComponent<PlayerCtrl>().AllowMovement();
        healthManager.ResetHealth();
        combatUI.SetActive(false);
        answerInput.interactable = false;
    }
}

