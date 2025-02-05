using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 5f;
    public float yOffset = 1f;
    public Transform target;

    void LateUpdate() // Exécute la mise à jour après le mouvement du joueur
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}

