using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CombatInProgress)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }
        body.linearVelocity = speed * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
