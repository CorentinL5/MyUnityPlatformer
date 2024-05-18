using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 8f;
    public float jumpForce = 21f;

    private bool isJumping = false;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask collisionLayers;


    [HideInInspector]
    public static float speedByButtons;
    [HideInInspector]
    public static bool pressedRight;
    [HideInInspector]
    public static bool pressedLeft;

    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    public static PlayerMovement instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;

        //récup les Composants automatiquement
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();

    }

    void Update()
    {
        UpdateSpeedByButtons();

        float horizontalInput = Input.GetAxis("Horizontal"); // Obtenez l'entrée du clavier
        float combinedSpeed = Mathf.Clamp(speedByButtons + horizontalInput, -1f, 1f); // Ajoutez l'entrée du clavier à la vitesse basée sur les boutons
        horizontalMovement = combinedSpeed * moveSpeed /** Time.deltaTime*/; // Appliquez la vitesse au mouvement horizontal
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        float characterVelocity = Mathf.Abs(rb.velocity.x); // valeur du mouve de X en absolu
        animator.SetFloat("Speed", characterVelocity);

        animator.SetFloat("YVelocity", rb.velocity.y);
        animator.SetBool("IsGrounded", isGrounded);
    }
    
    public void JumpButtonClick()
    {
        if (isGrounded)
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        
        MovePlayer(horizontalMovement);

        Flip(rb.velocity.x);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        //rb.velocity = new Vector2(_horizontalMovement, rb.velocity.y);

        if(isJumping)
        {
            //rb.AddForce(new Vector2(0f, jumpForce));
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
            isJumping = false;
        }
    }

    public void HitJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        } else if(_velocity < -0.1f)
         {
            spriteRenderer.flipX = true;
        }
    }

    private void UpdateSpeedByButtons()
    {
        float targetSpeed = 0f;

        if (pressedRight)
        {
            targetSpeed = 1f;
        }
        else if (pressedLeft)
        {
            targetSpeed = -1f;
        }

        speedByButtons = Mathf.Lerp(speedByButtons, targetSpeed, 0.25f);
        
        // Vérifiez si speedByButtons est suffisamment proche de 0
        if (Mathf.Abs(speedByButtons) < 0.01f)
        {
            speedByButtons = 0f;
        }
    }
}