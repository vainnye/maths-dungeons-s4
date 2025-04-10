using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private UnityEngine.UI.Button startButton;

    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogWarning("StartButton is not assigned in the inspector.");
        }
    }

    void OnStartButtonClicked()
    {
        if (player != null && playerSpawnPoint != null)
        {
            player.position = playerSpawnPoint.position;
        }
        else
        {
            Debug.LogWarning("Player or SpawnPoint not assigned.");
        }
    }
}
