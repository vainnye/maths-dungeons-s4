using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera arenaCamera;

    private void Start()
    {
        if (mainCamera != null)
        {
            mainCamera.orthographicSize *= 0.30f;
        }

        if (arenaCamera != null)
        {
            arenaCamera.orthographicSize *= 0.65f;
        }
    }
}
