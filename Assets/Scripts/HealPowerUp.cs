using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public int HealPoints;
    public AudioClip healPickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (PlayerHealth.currentHealth < PlayerHealth.maxHealth)
            {
                AudioManager.instance.PlayClipAt(healPickUpSound, transform.position);
                Destroy(gameObject);
                PlayerHealth.instance.HealPlayer(HealPoints);
            }
        }
    }
}
