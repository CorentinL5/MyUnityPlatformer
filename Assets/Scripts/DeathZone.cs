using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    private Animator fadeSystem;

    private PlayerHealth playerHealth;


    private void Awake()
    {       
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerHealth.Die();
        }
    }
}
