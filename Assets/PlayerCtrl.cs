using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float movSpeed = 5f; // Vitesse de déplacement
    private Rigidbody2D rb; // Référence au Rigidbody2D
    private Vector2 movement; // Stocke le mouvement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D
    }

    void Update()
    {
        // Récupère les entrées de déplacement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcule le mouvement
        movement = new Vector2(horizontal, vertical).normalized; // Normalise pour éviter d'aller plus vite en diagonale
    }

    void FixedUpdate()
    {
        // Applique la vitesse au Rigidbody pour respecter les collisions
        rb.velocity = movement * movSpeed;
    }
}
