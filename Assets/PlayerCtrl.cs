using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float movSpeed = 5f; // Vitesse de déplacement
    private Rigidbody2D rb; // Référence au Rigidbody2D
    private Vector2 movement; // Stocke le mouvement
    private bool canMove = true;  // Contrôle si le joueur peut se déplacer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D
    }

    void Update()
    {
        if (!canMove)
        {
            // Si le mouvement est bloqué, on ne fait rien dans Update
            return;
        }

        // Récupère les entrées de déplacement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcule le mouvement
        movement = new Vector2(horizontal, vertical).normalized; // Normalise pour éviter d'aller plus vite en diagonale
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            // Si le mouvement est bloqué, on met la vélocité à zéro
            rb.velocity = Vector2.zero;
            return;
        }

        // Applique la vitesse au Rigidbody pour respecter les collisions
        rb.velocity = movement * movSpeed;
    }

    // Méthode pour bloquer le mouvement du joueur
    public void BlockMovement()
    {
        canMove = false;  // Bloque les mouvements
        rb.velocity = Vector2.zero;  // Bloque aussi la vélocité immédiatement
    }

    // Méthode pour autoriser le mouvement du joueur
    public void AllowMovement()
    {
        canMove = true;  // Autorise les mouvements
    }
}
