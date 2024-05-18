using UnityEngine;
using System.Collections;


public class PlayerHealth : MonoBehaviour
{

    static public int maxHealth = 100;
    static public int currentHealth;

    public float invincibilityTimeAfterHit = 1f;
    public float invincibilityFlashDelay = 0.15f;
    public bool isInvincible = false;
    
    public AudioClip hitSound;
    public AudioClip DieSound;

    public SpriteRenderer graphics;
    private HealthBar healthBar;

    public static PlayerHealth instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerHealth dans la scène");
            return;
        }

        instance = this;

        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    /*void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }*/

    public void HealPlayer(int amount)
    {
        currentHealth = (currentHealth + amount) > maxHealth ? maxHealth : currentHealth + amount;
        healthBar.SetHealth(currentHealth);
    }


    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            AudioManager.instance.PlayClipAt(hitSound, transform.position);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth); // normalized met le chiffre entre 0 et 1



            //vérif si currenthealth est plus petit que 0
            if (currentHealth <= 0)
            {
                Die();
                return;
            }
            else
            {
                Hit();
            }
        }
    }

    public void Hit()
    {
        isInvincible = true;

        //AudioManager.instance.PlayClipAt(DieSound, transform.position);

        // Jouer l'animention de "hit"
        PlayerMovement.instance.animator.SetTrigger("Hit");

        StartCoroutine(HandleInvicibilityDelay());
    }

    public void Die()
    {
        AudioManager.instance.PlayClipAt(DieSound, transform.position);
        // Bloquer les mouvements du perso
        PlayerMovement.instance.enabled = false;

        // Jouer l'animention de "mort"
        PlayerMovement.instance.animator.SetTrigger("Die");

        // Limiter interactions physiques avec les autres éléments de la scène
        PlayerMovement.instance.rb.velocity = Vector3.zero;
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Kinematic;

        PlayerMovement.instance.playerCollider.enabled = false;

        GameOverManager.instance.OnPlayerDeath();
    }

    // Inverse de Die()
    public void Respawn()
    {
        PlayerMovement.instance.enabled = true;
        PlayerMovement.instance.animator.SetTrigger("Respawn");
        PlayerMovement.instance.rb.bodyType = RigidbodyType2D.Dynamic;
        PlayerMovement.instance.playerCollider.enabled = true;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    public IEnumerator HandleInvicibilityDelay()
    {    
        yield return new WaitForSeconds(invincibilityTimeAfterHit);
        isInvincible = false;
    }
}
