using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    // ATTENTION
    // pour que les menus fonctionnent ils ne doivent pas êtres désactivés par défaut dans l'inspecteur
    private const string pauseMenuUIName = "CanvasPauseMenu";
    private const string startMenuUIName = "CanvasStartMenu";
    private GameObject pauseMenuUI;
    private GameObject startMenuUI;

    private static string parentObjectName;

    public bool gameStarted { get; private set; } = false;
    public bool gamePaused { get; private set; } = false;
    public bool timePaused() { return Time.timeScale == 0f; }

    override protected void SingletonStarted()
    {
        base.SingletonStarted();
        if(pauseMenuUI == null) pauseMenuUI = GameObject.Find(pauseMenuUIName);
        if (startMenuUI == null) startMenuUI = GameObject.Find(startMenuUIName);
        if (pauseMenuUI == null) throw new Exception("impossible de trouver un objet nomé " + pauseMenuUIName + " dans la scène " + SceneManager.GetActiveScene().name);
        if (pauseMenuUI == null) throw new Exception("impossible de trouver un objet nomé " + startMenuUIName + " dans la scène " + SceneManager.GetActiveScene().name);
    }

    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // initialisation de la scène
        base.OnSceneLoaded(scene, mode);
        if (SceneManager.GetActiveScene().name != "MainScene") return;

        // gestion de l'initialisation de la scène après être revenu d'une autre scène
        //
        //Debug.Log("scene " + SceneManager.GetActiveScene().name + " is loading");
        //if (parentObjectName == null)
        //{
        //    parentObjectName = gameObject.name;
        //    Debug.Log("the parent object name of " + GetType().Name + " is " + parentObjectName);
        //}
        //GameObject.Find(parentObjectName).AddComponent<GameManager>(); // ça ne fonctionne pas

        if (pauseMenuUI == null) pauseMenuUI = GameObject.Find(pauseMenuUIName);
        if (startMenuUI == null) startMenuUI = GameObject.Find(startMenuUIName);
        if (pauseMenuUI == null) throw new Exception("impossible de trouver un objet nomé " + pauseMenuUIName + " dans la scène " + SceneManager.GetActiveScene().name);
        if (pauseMenuUI == null) throw new Exception("impossible de trouver un objet nomé " + startMenuUIName + " dans la scène " + SceneManager.GetActiveScene().name);

        pauseMenuUI.SetActive(false);
        startMenuUI.SetActive(true);
        
        PauseTime();
        
        //Debug.Log("scene " + SceneManager.GetActiveScene().name + " has been loaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("tab pressed");
            if (gamePaused)
                UnPauseGame();
            else if(gameStarted)
            {
                PauseGame();
                Debug.Log("tab pressed");
            }
        }
    }

    
    public void StartGame()
    {
        Debug.Log("starting game");
        gameStarted = true;
        UnPauseTime();
        startMenuUI.SetActive(false);
        Debug.Log("game started");
    }
    public void PauseGame()
    {
        Debug.Log("pausing game");
        gamePaused = true;
        pauseMenuUI.SetActive(true);
        PauseTime();
        Debug.Log("game paused");
    }
    public void UnPauseGame()
    {
        Debug.Log("resuming game : UnPause()"); 
        gamePaused = false;
        UnPauseTime();
        pauseMenuUI.SetActive(false);
        Debug.Log("game resumed : UnPause()");
    }
    public void PauseTime() { Time.timeScale = 0f; }
    public void UnPauseTime() { Time.timeScale = 1.0f; }
}
