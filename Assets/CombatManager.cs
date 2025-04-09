using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    [Header("Références générales")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject[] orcsInDungeon;

    [Header("UI Combat")]
    [SerializeField] private GameObject combatUI;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private UnityEngine.UI.Button submitButton;
    [SerializeField] private TMP_Text timerText;

    [Header("Game Over Panel")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Caméras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera arenaCamera;

    [Header("Temps limite")]
    [SerializeField] private float timeLimit = 30f;

    [Header("Difficulté du début")]
    [SerializeField] private int difficulty = 1;

    private bool combatInProgress = false;
    private int currentCorrectAnswer;
    private float currentTime;
    private GameObject currentOrcInDungeon; // Référence à l'orc dans le donjon

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera == null)
            Debug.LogError("La caméra principale n'est pas assignée.");

        if (arenaCamera == null)
            Debug.LogError("ArenaCamera est introuvable ou non assignée.");

        if (gameOverPanel != null)
            gameOverPanel.gameObject.SetActive(false);  // Assurez-vous que le message GameOver est caché au départ
    }

    // Modification ici : ajout d'un paramètre pour l'orc dans le donjon
    public void StartCombat(GameObject orcInDungeon)
    {
        currentOrcInDungeon = orcInDungeon; // Assignation de l'orc spécifié
        GameObject playerArenaModel = GameObject.Find("PlayerArena");
        GameObject orcArenaModel = GameObject.Find("OrcArena");

        if (playerArenaModel == null || orcArenaModel == null)
        {
            Debug.LogError("Les modèles PlayerArena ou OrcArena sont introuvables !");
            return;
        }

        if (combatUI == null || questionText == null || answerInput == null || submitButton == null)
        {
            Debug.LogError("Une ou plusieurs références UI sont manquantes !");
            return;
        }

        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(() => CheckAnswer());

        // Switch caméras
        mainCamera?.gameObject.SetActive(false);
        if (mainCamera?.TryGetComponent(out AudioListener mainListener) == true)
            mainListener.enabled = false;

        arenaCamera?.gameObject.SetActive(true);
        if (arenaCamera?.TryGetComponent(out AudioListener arenaListener) == true)
            arenaListener.enabled = true;

        // Bloquer les mouvements du joueur
        playerArenaModel.GetComponent<PlayerCtrl>()?.BlockMovement();

        // Afficher UI
        combatUI.SetActive(true);
        timerText.gameObject.SetActive(true);
        answerInput.interactable = true;
        submitButton.interactable = true;
        answerInput.text = "";
        answerInput.Select();
        answerInput.ActivateInputField();

        combatInProgress = true;

        // Définir le temps limite selon la difficulté
        if (difficulty == 1)
        {
            timeLimit = 30f;  // 30 secondes pour la difficulté 1
        }
        else if (difficulty == 2)
        {
            timeLimit = 50f;  // 50 secondes pour la difficulté 2
        }
        else if (difficulty == 3)
        {
            timeLimit = 100f;  // 100 secondes pour la difficulté 3
        }
        else if (difficulty == 4)
        {
            timeLimit = 200f;  // 200 secondes pour la difficulté 4
        }

        currentTime = timeLimit;
        GenerateQuestion(); // Ça initialise aussi le timer maintenant
    }

    private void Update()
    {
        if (!combatInProgress) return;

        currentTime -= Time.deltaTime;
        timerText.text = Mathf.Ceil(currentTime) + "s";

        if (currentTime <= 0)
        {
            Debug.Log("Temps écoulé !");
            HealthManager healthManager = FindObjectOfType<HealthManager>();

            // Mort immédiate du joueur
            healthManager.TakeDamageToPlayer(500);

            if (healthManager.IsPlayerDead())
            {
                EndCombat(false);
            }
            else
            {
                GenerateQuestion(); // On relance la question s'il survit (rare)
            }
        }
    }

    private void GenerateQuestion()
    {
        int num1, num2, num3, operation;
        float result;

        // Difficulté 1
        if (difficulty == 1)
        {
            // Niveau 1 et 2 : Addition, soustraction, multiplication simples
            num1 = Random.Range(1, 10);
            num2 = Random.Range(1, 10);
            operation = Random.Range(1, 4);

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
        }
        // Difficulté 2
        else if (difficulty == 2)
        {
            // Niveau 3 : Expressions avec parenthèses et calculs plus complexes
            num1 = Random.Range(1, 10);
            num2 = Random.Range(1, 10);
            num3 = Random.Range(1, 10);
            operation = Random.Range(1, 3); // 1: multiplication, 2: division

            if (operation == 1)
            {
                // Exemple : "Combien fait (5 + 3) x 2 ?"
                currentCorrectAnswer = (num1 + num2) * num3;
                questionText.text = $"Combien fait ({num1} + {num2}) x {num3} ?";
            }
            else if (operation == 2)
            {
                // Exemple : "Quel est le résultat de 8 ÷ (2 + 2) ?"
                int divResult = num1 + num2; // Le diviseur est une somme
                currentCorrectAnswer = (num1) / divResult;
                questionText.text = $"Quel est le résultat de {num1} ÷ ({num2} + {num3}) ?";
            }
        }
        // Difficulté 3
        else if (difficulty == 3)
        {
            // Niveau 4 : Résolution d'équations complexes
            num1 = Random.Range(1, 10);  // Premier terme
            num2 = Random.Range(1, 10);  // Deuxième terme
            num3 = Random.Range(1, 10);  // Troisième terme
            operation = Random.Range(1, 5); // 1: équation linéaire simple, 2: équation avec multiplication, 3: système d'équations, 4: équation quadratique

            if (operation == 1)
            {
                // Exemple : 3x + 5 = 20
                int coeff = Random.Range(1, 5);  // Coefficient pour x
                int constant = Random.Range(1, 10);  // Terme constant
                int resultat = Random.Range(10, 30); // La constante de droite (ex: 20)
                currentCorrectAnswer = (resultat - constant) / coeff;  // Résolution de l'équation
                questionText.text = $"Résoudre {coeff}x + {constant} = {resultat}. Quelle est la valeur de x ?";
            }
            else if (operation == 2)
            {
                // Exemple : 4x - 2 = 3(x + 5)
                int coeff1 = Random.Range(1, 5);  // Coefficient de x à gauche
                int coeff2 = Random.Range(1, 5);  // Coefficient de x à droite
                int constant1 = Random.Range(1, 10);  // Terme constant à gauche
                int constant2 = Random.Range(1, 10);  // Terme constant à droite
                                                      // Résolution de l'équation : 4x - 2 = 3(x + 5)
                int leftSide = coeff1 * num1 - constant1;
                int rightSide = coeff2 * (num1 + constant2);
                currentCorrectAnswer = (constant2 - constant1) / (coeff1 - coeff2);  // Résolution de l'équation
                questionText.text = $"Résoudre {coeff1}x - {constant1} = {coeff2}(x + {constant2}). Quelle est la valeur de x ?";
            }
            else if (operation == 3)
            {
                // Exemple : Résoudre un système d'équations
                int constant1 = Random.Range(1, 10); // Terme constant pour la première équation
                int constant2 = Random.Range(1, 10); // Terme constant pour la seconde équation

                // Calcul de x et y de manière à ce que les réponses soient entières
                int x = Random.Range(1, 10); // x entre 1 et 10
                int y = Random.Range(1, 10); // y entre 1 et 10

                // Créer un système d'équations linéaires
                int equation1Left = x + y;  // Première équation : x + y = ? (ex: 7)
                int equation2Left = 2 * x - y;  // Deuxième équation : 2x - y = ? (ex: 10)

                // La somme des deux équations donnera la bonne réponse
                currentCorrectAnswer = equation1Left;  // On utilise le résultat de la première équation

                questionText.text = $"Résoudre le système d'équations : x + y = {equation1Left}, 2x - y = {equation2Left}. Quelle est la valeur de x + y ?";
            }
            else if (operation == 4)
            {
                // Exemple : x² + 2x - 3 = 0
                int a = Random.Range(1, 5);  // Coefficient de x²
                int b = Random.Range(1, 5);  // Coefficient de x
                int c = Random.Range(-10, 10);  // Terme constant
                                                // Résoudre l'équation quadratique : ax² + bx + c = 0
                int discriminant = b * b - 4 * a * c; // Discriminant pour vérifier si il existe une solution réelle
                if (discriminant >= 0)
                {
                    // Si le discriminant est positif ou nul, nous avons des solutions réelles
                    int sqrtDiscriminant = Mathf.FloorToInt(Mathf.Sqrt(discriminant));
                    int x1 = (-b + sqrtDiscriminant) / (2 * a);
                    int x2 = (-b - sqrtDiscriminant) / (2 * a);
                    questionText.text = $"Résoudre l'équation : {a}x² + {b}x + {c} = 0. Quelle est la valeur de x ?";
                    currentCorrectAnswer = x1; // Choisir x1 comme solution
                }
                else
                {
                    // Si le discriminant est négatif, il n'y a pas de solutions réelles, vous pouvez gérer cela différemment
                    questionText.text = $"L'équation {a}x² + {b}x + {c} = 0 n'a pas de solution réelle.";
                    currentCorrectAnswer = int.MinValue;  // Indiquer qu'il n'y a pas de solution réelle
                }
            }
        }
        // Difficulté 4
        else if (difficulty == 4)
        {
            // Mathématiques universitaires (niveau avancé)
            num1 = Random.Range(1, 50);
            num2 = Random.Range(1, 50);
            num3 = Random.Range(1, 50);
            operation = Random.Range(1, 4); // 1: intégrale, 2: dérivée, 3: systèmes d'équations

            if (operation == 1)
            {
                // Exemple de calcul intégral (simplifié pour l'exemple)
                result = num1 * Mathf.Pow(num2, 2) / 2f;
                currentCorrectAnswer = Mathf.RoundToInt(result);
                questionText.text = $"Intégrale de {num1}x² dx entre 0 et {num2}. Quel est le résultat ?";
            }
            else if (operation == 2)
            {
                // Exemple de dérivée (simplifiée)
                result = 2 * num1 * num2;
                currentCorrectAnswer = Mathf.RoundToInt(result);
                questionText.text = $"Quelle est la dérivée de {num1}x² ?";
            }
            else if (operation == 3)
            {
                // Exemple de système d'équations : x + y = 10, 2x - y = 4
                currentCorrectAnswer = num1 + num2;
                questionText.text = $"Résoudre le système d'équations : x + y = 10, 2x - y = 4. Quelle est la valeur de x + y ?";
            }
        }

        // Réinitialiser le champ de réponse
        answerInput.text = "";
        Debug.Log($"Nouvelle question : {questionText.text} (Réponse : {currentCorrectAnswer})");
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
                healthManager.TakeDamageToPlayer(100); // Mauvaise réponse
            }

            if (healthManager.IsOrcDead())
            {
                EndCombat(true);
                if (currentOrcInDungeon != null)
                {
                    currentOrcInDungeon.SetActive(false); // Désactiver l'orc dans le donjon
                }
                CheckDungeonStatus(); // Vérifier si tous les orcs sont morts
            }
            else if (healthManager.IsPlayerDead())
            {
                EndCombat(false);
            }
            else
            {
                GenerateQuestion(); // Question suivante
            }
        }
    }

    private void EndCombat(bool playerWon)
    {
        HealthManager healthManager = FindObjectOfType<HealthManager>();
        Debug.Log(playerWon ? "Victoire !" : "Défaite...");
        combatInProgress = false;
        combatUI.SetActive(false);

        // Switch caméras
        arenaCamera?.gameObject.SetActive(false);
        if (arenaCamera?.TryGetComponent(out AudioListener arenaListener1) == true)  // Renommé en arenaListener1
            arenaListener1.enabled = false;

        mainCamera?.gameObject.SetActive(true);
        if (mainCamera?.TryGetComponent(out AudioListener mainListener) == true)
            mainListener.enabled = true;

        if (playerWon)
        {
            // Revenir dans le donjon, hp du joueur conservé, orc reset la prochaine fois
            Debug.Log("Retour au donjon, prêt pour le prochain combat.");
            // Reset vie de l'orc
            healthManager.ResetOrcHealth();
        }
        else
        {
            // Mettre la caméra d'arène en mode combat
            if (arenaCamera != null)
            {
                arenaCamera.gameObject.SetActive(true);  // Active la caméra d'arène
                if (arenaCamera.TryGetComponent(out AudioListener arenaListener2))  // Renommé en arenaListener2
                {
                    arenaListener2.enabled = true;  // Active l'AudioListener sur l'arène
                }
            }

            // Afficher Game Over
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
            Time.timeScale = 0;  // Stop le jeu lorsque le joueur meurt
        }
    }


    private void CheckDungeonStatus()
    {
        bool allOrcsDead = true;
        int remainingOrcs = 0;  // Variable pour compter les orcs restants

        foreach (var orc in orcsInDungeon)
        {
            if (orc.activeInHierarchy)  // Si l'orc est encore actif
            {
                allOrcsDead = false;
                remainingOrcs++;  // Incrémente le nombre d'ennemis restants
            }
        }

        Debug.Log($"Orcs restants : {remainingOrcs}");

        if (allOrcsDead)
        {
            Debug.Log("Tous les orcs sont morts ! Le joueur a gagné.");
            // Ajouter la logique pour le passage au niveau suivant ou faire réapparaître les orcs avec une difficulté accrue.
            difficulty++;
            respawnOrcs();
        }
    }

    private void respawnOrcs()
    {
        foreach (var orc in orcsInDungeon)
        {
            if (orc != null)
            {
                orc.SetActive(true);  // Réactive l'orc
            }
        }
        // Vous pouvez aussi réinitialiser d'autres aspects comme la santé des orcs, la difficulté, etc. si nécessaire.
        Debug.Log("Les orcs sont de nouveau activés !");
    }
}
