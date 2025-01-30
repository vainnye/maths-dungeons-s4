using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private float movSpeed = 5f; // Vitesse de d�placement
    private Rigidbody2D rb; // R�f�rence au Rigidbody2D
    private Vector2 movement; // Stocke le mouvement

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // R�cup�re le Rigidbody2D
    }

    void Update()
    {
        // R�cup�re les entr�es de d�placement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calcule le mouvement
        movement = new Vector2(horizontal, vertical).normalized; // Normalise pour �viter d'aller plus vite en diagonale
    }

    void FixedUpdate()
    {
        // Applique la vitesse au Rigidbody pour respecter les collisions
        rb.velocity = movement * movSpeed;
    }
}
