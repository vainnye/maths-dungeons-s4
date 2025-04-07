using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    [Header("Références générales")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject orcPrefab;
    [SerializeField] private string arenaSceneName = "ArenaScene";
    private GameObject playerInstance;
    private GameObject orcInstance;

    [Header("UI Combat")]
    [SerializeField] private GameObject combatUI;
    [SerializeField] private Text questionText;
    [SerializeField] private InputField answerInput;
    [SerializeField] private Button submitButton;

    private int currentCorrectAnswer;
    private string previousSceneName;

    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    public void StartCombat()
    {
        previousSceneName = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnArenaSceneLoaded;
        SceneManager.LoadScene(arenaSceneName);
    }

    private void OnArenaSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == arenaSceneName)
        {
            SceneManager.sceneLoaded -= OnArenaSceneLoaded;
            StartCoroutine(InitializeCombatAfterSceneLoad());
        }
    }

    private IEnumerator InitializeCombatAfterSceneLoad()
    {
        yield return null; // attendre 1 frame

        // Attendre que HealthManager soit dispo
        HealthManager healthManager = null;
        int maxTries = 30;
        while (healthManager == null && maxTries-- > 0)
        {
            healthManager = FindObjectOfType<HealthManager>();
            yield return null;
        }

        if (healthManager == null)
        {
            Debug.LogError("HealthManager introuvable !");
            yield break;
        }

        // Idem pour les spawns
        Transform playerSpawn = GameObject.Find("ArenaPlayerSpawn")?.transform;
        Transform orcSpawn = GameObject.Find("ArenaEnemySpawn")?.transform;

        if (playerSpawn == null || orcSpawn == null)
        {
            Debug.LogError("Spawn points non trouvés !");
            yield break;
        }

        // Ensuite tu continues avec l’instanciation :
        playerInstance = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        orcInstance = Instantiate(orcPrefab, orcSpawn.position, Quaternion.identity);

        // UI setup
        combatUI = GameObject.Find("CombatUI");
        questionText = GameObject.Find("QuestionText").GetComponent<Text>();
        answerInput = GameObject.Find("AnswerInput").GetComponent<InputField>();
        submitButton = GameObject.Find("SubmitButton").GetComponent<Button>();

        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => CheckAnswer(healthManager));

        playerInstance.GetComponent<PlayerCtrl>().BlockMovement();
        combatUI.SetActive(true);
        answerInput.interactable = true;
        submitButton.interactable = true;
        answerInput.text = "";
        answerInput.Select();
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

    private void CheckAnswer(HealthManager healthManager)
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

        combatUI.SetActive(false);

        Destroy(playerInstance);
        Destroy(orcInstance);

        SceneManager.LoadScene(previousSceneName);
    }
}
