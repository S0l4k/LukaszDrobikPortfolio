using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private TextMesh textMesh;
    [SerializeField] private SpriteRenderer buttonERenderer;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AxeThrow axeThrow;
    [SerializeField] private Rigidbody2D rb;
    private int i = 0;
    private bool talkable = false;
    private bool talking = false;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && talkable == true)
        {
            textMesh.text = dialogueLines[i];
            i++;
            talkable = false;
            talking = true;
            playerMovement.enabled = false;
            axeThrow.enabled = false;
            rb.velocity = new Vector3(0, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.E) && talking == true && i < dialogueLines.Length)
        {
            textMesh.text = dialogueLines[i];
            i++;
        }
        else if(Input.GetKeyDown(KeyCode.E) && i == dialogueLines.Length && talking == true)
        {
            talking = false;
            i = 0;
            textMesh.text = "";
            playerMovement.enabled = true;
            axeThrow.enabled = true;
            talkable = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            buttonERenderer.enabled = true;
            talkable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            buttonERenderer.enabled = false;
            talkable = false;
        }
    }
}
