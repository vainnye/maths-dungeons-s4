using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private GameObject difficultySelectionUI;
    [SerializeField] private Slider difficultySlider;
    [SerializeField] private Button buttonGM_Training;
    [SerializeField] private Button buttonGM_Dungeon;
    [SerializeField] private TMP_Text difficultyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        difficultySlider.onValueChanged.AddListener((val) => { difficultyText.text = ((int)val).ToString(); });
    }

    private void OnEnable()
    {
        ChooseGM_Training();
    }

    public void ChooseGM_Training()
    {
        difficultySelectionUI.SetActive(true);
        //ChangeColor(buttonGM_Training, Color.grey);
        //ChangeColor(buttonGM_Dungeon, Color.white);
        GameManager.Instance.GameMode = GameManager.GMode.TRAINING;
    }

    public void ChooseGM_Dungeon()
    {
        difficultySelectionUI.SetActive(false);
        //ChangeColor(buttonGM_Dungeon, Color.grey);
        //ChangeColor(buttonGM_Training, Color.white);
        GameManager.Instance.GameMode = GameManager.GMode.DUNGEON;
    }

    public void StartGame()
    {
        if(GameManager.Instance.GameMode == GameManager.GMode.DUNGEON)
        {
            GameManager.Instance.Difficulty = 1;
        }
        else if (GameManager.Instance.GameMode == GameManager.GMode.TRAINING)
        {
            GameManager.Instance.Difficulty = (int) difficultySlider.value;
        }
        GameManager.Instance.StartGame();
    }

    static void ChangeColor(Button btn, Color color)
    {
        ColorBlock cb = btn.colors;
        cb.normalColor = color;
        btn.colors = cb;
    }
}
