using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float movSpeed = 5f; // Vitesse de d�placement
    private Rigidbody2D rb; // R�f�rence au Rigidbody2D
    private Vector2 movement; // Stocke le mouvement
    private bool canMove = true;  // Contr�le si le joueur peut se d�placer

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // R�cup�re le Rigidbody2D
    }

    void Update()
    {
        if (!canMove)
        {
            // Si le mouvement est bloqu�, on ne fait rien dans Update
            return;
        }

        // R�cup�re les entr�es de d�placement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcule le mouvement
        movement = new Vector2(horizontal, vertical).normalized; // Normalise pour �viter d'aller plus vite en diagonale
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            // Si le mouvement est bloqu�, on met la v�locit� � z�ro
            rb.velocity = Vector2.zero;
            return;
        }

        // Applique la vitesse au Rigidbody pour respecter les collisions
        rb.velocity = movement * movSpeed;
    }

    // M�thode pour bloquer le mouvement du joueur
    public void BlockMovement()
    {
        canMove = false;  // Bloque les mouvements
        rb.velocity = Vector2.zero;  // Bloque aussi la v�locit� imm�diatement
    }

    // M�thode pour autoriser le mouvement du joueur
    public void AllowMovement()
    {
        canMove = true;  // Autorise les mouvements
    }
}
