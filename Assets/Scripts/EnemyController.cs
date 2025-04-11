using System;
using UnityEditorInternal;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    [SerializeField] private float move;
    [SerializeField] private float speed;
    private string axis;
    private float origin;
    private float lastPos;
    private float direction;

    private Rigidbody2D body;

    private void Awake()
    {
        axis = UnityEngine.Random.value < 0.5f ? "X" : "Y";

        origin = GetPos();
        move = Mathf.Abs(move);
        speed = Mathf.Abs(speed);
        direction = origin + move * (UnityEngine.Random.value < 0.5f ? -1f : 1f); // la direction originale
        body = GetComponent<Rigidbody2D>();
        
        body.freezeRotation = true;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (GameManager.Instance.CombatInProgress)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }

        if (!(origin - Mathf.Abs(direction) < GetPos() && GetPos() < origin + Mathf.Abs(direction)) )
        {
            direction *= -1; // on inverse la direction
        }
        float dist = Mathf.Sign(direction) * speed * Time.deltaTime;
        
        lastPos = GetPos();
        
        Translate(dist);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.StartCombat(gameObject); // démarrer le comabt avec l'ennemi
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("stay");
            direction *= -1; // flip the direction if it can't go farther
        }
    }


    private float GetPos()
    {
        switch (axis)
        {
            case "X":
                return gameObject.transform.position.x;
            case "Y":
                return gameObject.transform.position.y;
            default:
                throw new System.Exception("axe invalide");
        }
    }

    private void Translate(float dist)
    {
        switch (axis)
        {
            case "X":
                gameObject.transform.Translate(new Vector3(dist, 0, 0));
                break;
            case "Y":
                gameObject.transform.Translate(new Vector3(0, dist, 0));
                break;
            default:
                throw new System.Exception("invalid axis");
        }
    }
}
