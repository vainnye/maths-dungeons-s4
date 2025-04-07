using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    //private Vector2 motion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //motion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //transform.Translate(motion * speed * Time.deltaTime);
        body.linearVelocity = speed * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
