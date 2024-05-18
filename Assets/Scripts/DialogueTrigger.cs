using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    private bool isInRange = false;

    private Text interactUIText;
    private Button interactUIButton;

    void Awake()
    {
        interactUIText = GameObject.FindGameObjectWithTag("InteractUI")?.transform.GetChild(0).GetComponent<Text>();
        interactUIButton = GameObject.FindGameObjectWithTag("InteractUI")?.GetComponent<Button>();

        interactUIButton?.onClick.AddListener(() => TriggerDialogue());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        if (isInRange)
        {
            DialogueManager.instance.StartDialogue(dialogue);
            interactUIText.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUIText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            interactUIText.enabled = false;
            DialogueManager.instance.EndDialogue();
        }
    }
}
