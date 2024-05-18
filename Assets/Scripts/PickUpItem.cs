using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    private Text interactUIText;
    private Button interactUIButton;
    private bool isInRange;

    public Item item;
    public AudioClip soundToPlay;

    void Awake()
    {
        interactUIText = GameObject.FindGameObjectWithTag("InteractUI")?.transform.GetChild(0).GetComponent<Text>();
        interactUIButton = GameObject.FindGameObjectWithTag("InteractUI")?.GetComponent<Button>();

        interactUIButton?.onClick.AddListener(() => TakeItem());
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeItem();
        }
    }
    public void TakeItem()
    {
        if (isInRange)
        {
            AudioManager.instance.PlayClipAt(soundToPlay, transform.position);

            Inventory.instance.content.Add(item);
            Inventory.instance.UpdateInventoryUI();
            interactUIText.enabled = false;
            Destroy(gameObject);   
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
