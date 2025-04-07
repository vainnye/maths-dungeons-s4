using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ArenaScripts : MonoBehaviour
{
    [SerializeField] string mainSceneName = "MainScene";

    public void toMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }
}
