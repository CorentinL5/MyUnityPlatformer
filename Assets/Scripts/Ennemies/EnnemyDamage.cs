using UnityEngine;

public class EnnemyDamage : MonoBehaviour
{
    public int damageOnCollision = 10;
    public AudioClip damageSound;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayClipAt(damageSound, transform.position);
            PlayerHealth.instance.TakeDamage(damageOnCollision);
        }
    }
}
