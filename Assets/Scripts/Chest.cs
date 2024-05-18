using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chest : MonoBehaviour
{
    private Text interactUIText;
    private Button interactUIButton;
    private bool isInRange;

    public Animator animator;
    public int coinsToAdd = 5;
    public AudioClip soundToPlay;

    void Awake()
    {
        interactUIText = GameObject.FindGameObjectWithTag("InteractUI")?.transform.GetChild(0).GetComponent<Text>();
        interactUIButton = GameObject.FindGameObjectWithTag("InteractUI")?.GetComponent<Button>();

        interactUIButton?.onClick.AddListener(() => OpenChest());
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }
    public void OpenChest()
    {
        if (isInRange)
        {
            animator.SetTrigger("OpenChest");
            Inventory.instance.AddCoins(coinsToAdd);
            AudioManager.instance.PlayClipAt(soundToPlay, transform.position);
            GetComponent<BoxCollider2D>().enabled = false;
            if (interactUIText != null)
            {
                interactUIText.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactUIText != null)
            {
                interactUIText.enabled = true;
            }
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactUIText != null)
            {
                interactUIText.enabled = false;
            }
            isInRange = false;
        }
    }
}
