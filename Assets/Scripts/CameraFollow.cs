using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float timeOffset;
    [SerializeField] private Vector3 posOffset;

    private Vector3 velocity;

    void Update()
    {
        Vector3 desiredPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z) + posOffset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, timeOffset);
    }
}
