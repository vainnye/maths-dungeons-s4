using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnnemyControllerScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Arena");
        }
    }
}
