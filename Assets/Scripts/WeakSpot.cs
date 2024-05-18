using UnityEngine;
using System.Collections;

public class WeakSpot : MonoBehaviour
{

    private GameObject objectToDestroy;

    private void Awake()
    {
        objectToDestroy = transform.parent?.gameObject;
;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Détruisez l'objet.
            EnnemyDeath.instance.EnnemyDie(objectToDestroy);

            EnnemyPatrol ennemyPatrol = transform.parent.gameObject.GetComponent<EnnemyPatrol>();
                if (ennemyPatrol != null)
                {
                    ennemyPatrol.EnnemyDied = true;
                }

            // Désactiver ce WeakSpot
            gameObject.SetActive(false);
        }
    }
}