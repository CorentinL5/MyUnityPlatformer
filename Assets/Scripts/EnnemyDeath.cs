using UnityEngine;
using System.Collections;

public class EnnemyDeath : MonoBehaviour
{
    public static EnnemyDeath instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de EnnemyDeath dans la scène");
            return;
        }

        instance = this;
    }

    public void EnnemyHit(GameObject ennemy)
    {
        // Jouer l'animation de hit.
        ennemy.GetComponent<Animator>().SetTrigger("Hit");
        
        // Faire sauter le joueur
        PlayerMovement.instance.HitJump();
    }

    public void EnnemyDie(GameObject ennemy)
    {
        // Retirer la collision.
        ennemy.GetComponent<CapsuleCollider2D>().enabled = false;

        // Jouer l'animation de hit.
        EnnemyHit(ennemy);

        // Modifier la masse et faire "sauter" l'ennemy
        ennemy.GetComponent<Rigidbody2D>().mass = 0.1f;
        ennemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-5f, 5f), 15f));

        
        StartCoroutine(RotateEnemy(ennemy)); // Effectue une rotation et Destroy l'objet.
    }


   private IEnumerator RotateEnemy(GameObject ennemy)
    {
        // Définir les angles d'Euler pour la rotation aléatoire entre -135 et 135 degrés autour de l'axe Z.
        Vector3 randomRotationEuler = new Vector3(0f, 0f, Random.Range(-135f, 135f));
        Quaternion startRotation = ennemy.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(randomRotationEuler);

        float elapsedTime = 0f;
        float duration = 1.25f; // 1 seconde

        while (elapsedTime < duration)
        {
            ennemy.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // Attendre la prochaine frame.
        }

        // Assurez-vous que la rotation est exactement à la cible à la fin.
        ennemy.transform.rotation = targetRotation;

        // Détruire l'objet après x temps après la rotation.
        Destroy(ennemy, 1);
    }
}
