using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } = null;
    // ATTENTION
    // pour que les menus fonctionnent ils ne doivent pas êtres désactivés par défaut dans l'inspecteur
    [Header("User Interfaces")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameWonUI;
    [SerializeField] private GameObject arenaUI;
    [SerializeField] private GameObject overlayUI;

    [Header("UI elements")]
    [SerializeField] private TMP_Text difficultyText;
    [SerializeField] private TMP_Text progressText;

    [Header("Cameras")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject arenaCamera;

    [Header("Scripts")]
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private CombatManager arena;


    private const int MAX_DIFF_IMPLEMENTEE = 4;

    [Range(1, MAX_DIFF_IMPLEMENTEE)]
    [SerializeField] private int startDifficulty = 1;
    
    public enum GMode
    {
        TRAINING,
        DUNGEON
    }

    public int Difficulty { get; set; }
    public GMode GameMode { get; set; }
    public bool CombatInProgress { get; set; } = false;
    public bool GameStarted { get; private set; } = false;
    public bool GamePaused { get; private set; } = false;
    public bool TimePaused() { return Time.timeScale == 0f; }

    private void Awake()
    {
        if (Instance != null) throw new Exception("Il ne devrait exister qu'une seule instance de GameManager, une deuxième instance a été créée");
        Instance = this;
    }

    private void Start()
    {
        PauseTime();
        pauseMenuUI.SetActive(false);
        startMenuUI.SetActive(true);
        arenaUI.SetActive(false);
        gameOverUI.SetActive(false);
        overlayUI.SetActive(true);

        mainCamera.SetActive(true);
        arenaCamera.SetActive(false);

        Difficulty = startDifficulty;
        ShowDifficulty();
    }


    // Update is called once per frame
    void Update()
    {
        if (CombatInProgress) return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("tab pressed");
            if (GamePaused)
                UnPauseGame();
            else if(GameStarted)
            {
                PauseGame();
                Debug.Log("tab pressed");
            }
        }
    }


    public bool NextDifficulty()
    {
        if (GameMode == GMode.TRAINING) throw new Exception("NextDifficulty n'est pas disponible dans le gamemode entrainement");

        if(Difficulty == MAX_DIFF_IMPLEMENTEE)
        {
            Debug.Log("vous avez entièrement terminé le jeu, il n'y a pas de difficultés supplémentaires");
            return false;
        }
        else Difficulty++;
        switch(Difficulty)
        {
            case 1:
                spawner.maxEnemies = 20;
                break;
            case 2:
                spawner.maxEnemies = 15;
                break;
            case 3:
                spawner.maxEnemies = 10;
                break;
            case 4:
                spawner.maxEnemies = 5;
                break;
        }
        ShowDifficulty();
        return true;
    }


    private void ShowDifficulty()
    {
        difficultyText.text = "Diff. " + Difficulty.ToString();
    }

    public void StartGame()
    {
        Debug.Log("starting game");

        GameStarted = true;
        UnPauseTime();
        startMenuUI.SetActive(false);
        spawner.doRespawn = (GameMode == GMode.TRAINING);
        if (GameMode == GMode.TRAINING)
        {
            spawner.maxEnemies = 100;
            spawner.spawnInterval = 1;
        }
        else
        {
            Difficulty = 0;
            NextDifficulty();
            spawner.spawnInterval = 0.5f;
        }
        spawner.StartNewWave();
        
        ShowDifficulty();

        Debug.Log("game started");
    }
    public void PauseGame()
    {
        Debug.Log("pausing game");
        GamePaused = true;
        pauseMenuUI.SetActive(true);
        PauseTime();
        Debug.Log("game paused");
    }
    public void UnPauseGame()
    {
        Debug.Log("resuming game : UnPause()"); 
        GamePaused = false;
        UnPauseTime();
        pauseMenuUI.SetActive(false);
        Debug.Log("game resumed : UnPause()");
    }
    public void PauseTime() { Time.timeScale = 0f; }
    public void UnPauseTime() { Time.timeScale = 1.0f; }


    public void Kill(GameObject enemy)
    {
        if(!spawner.NotifyDestroy(enemy)) Destroy(enemy); // on tue l'ennemi via le spwaner s'il a été créé avec le spawner sinon on utilise Destroy()
        ShowProgress();
    }

    public void ShowProgress()
    {
        progressText.text = $"tués : {spawner.NbSpawnedInGame-spawner.NbLiving}\nrestants : {spawner.NbLiving}";
    }

    public void StartCombat(GameObject enemy)
    {
        spawner.StopSpawning();

        arenaUI.SetActive(true);
        SwitchCamere(mainCamera, arenaCamera);
        arena.StartCombat(enemy);
    }

    public void EndCombat(bool playerWon)
    {
        arenaUI.SetActive(false);
        if (!playerWon) gameOverUI.SetActive(true); // fin du jeu
        else // retourne au donjon
        {
            Debug.Log("Retour au donjon, prêt pour le prochain combat.");
            SwitchCamere(arenaCamera, mainCamera);
            if(GameMode == GMode.DUNGEON)
            {
                if (spawner.NbLiving == 0)
                {
                    if (!NextDifficulty())
                    {
                        Debug.Log("Tous les orcs sont morts ! Le joueur a gagné.");
                        gameWonUI.SetActive(true);
                        return;
                    }
                    else
                    {
                        spawner.StartNewWave();
                    }
                }
                else
                {
                    spawner.SpawnEnemies();
                }
            }
            else if(GameMode == GMode.TRAINING)
            {
                spawner.SpawnEnemies();
            }
        }
    }

    public bool AllEnemiesDefeated()
    {
        Debug.Log($"Orcs restants : {GameObject.FindGameObjectsWithTag("Enemy").Length}");

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
            return true;
        return false;
    }


    private void SwitchCamere(GameObject from, GameObject to)
    {
        //if (from.GetComponent<Camera>() == null) throw new Exception("the GameObject to switch from is not a valid Camera");
        //if (to.GetComponent<Camera>() == null) throw new Exception("the GameObject to switch from is not a valid Camera");
        
        from.SetActive(false);
        if (from.TryGetComponent(out AudioListener fromAudio) == true)
            fromAudio.enabled = false;

        to.SetActive(true);
        if (to.TryGetComponent(out AudioListener toAudio) == true)
            toAudio.enabled = true;
    }
}
