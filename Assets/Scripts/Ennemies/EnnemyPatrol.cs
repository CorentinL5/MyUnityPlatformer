using System;
using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 0.5f;

    public Transform groundDetection;
    public LayerMask collisionLayers;
    public bool EnnemyDied = false;

    private bool movinRight = true;
    private bool speedIsChanging = false;

    private SpriteRenderer graphics;
    private Rigidbody2D rb;

    void Start()
    {
        graphics = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(!EnnemyDied)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance, collisionLayers);
            var frontInfo = Physics2D.OverlapCircle(groundDetection.position, distance / 3, collisionLayers);

            if (!groundInfo.collider || frontInfo)
            {
                ReverseSpeed(groundInfo.collider != null); // Passer un booléen pour indiquer la collision avec le sol
                movinRight = !movinRight; // Utilisation d'une logique plus concise pour inverser la direction
            }

            Flip(rb.velocity.x);
        }
    }

    private void Flip(float _velocity)
    {
        if (-_velocity > 0.1f)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void ReverseSpeed(bool groundCollision)
    {
        if (!speedIsChanging)
        {
            speedIsChanging = true;
            speed = -speed;
            rb.velocity = Vector3.zero;
        }
        speedIsChanging = !groundCollision;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundDetection.position, groundDetection.position + Vector3.down * distance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundDetection.position, distance/3);
    }
}