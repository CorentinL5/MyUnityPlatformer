using UnityEngine;

public class StartArrow : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Animator>().SetTrigger("FlagMove");

        }
    }
}
