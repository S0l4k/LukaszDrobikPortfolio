using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingScript : MonoBehaviour
{
    [Header ("Scripts")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AxeThrow axeThrow;

    [Header("Components")]
    private SpriteRenderer buttonERenderer;
    private SpriteRenderer playerRenderer;
    [SerializeField] private GameObject buttonObjectE;
    private Rigidbody2D rb;
    private BoxCollider2D hideCollider;
    [SerializeField] private LayerMask jumpGround;

    [Header("Variables")]
    [SerializeField] private bool hidable;
    static public bool hiding;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hideCollider = GetComponent<BoxCollider2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        buttonERenderer = buttonObjectE.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        buttonERenderer.enabled = false;
        hidable = false;
        hiding = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Hiding
        if (hidable == true && Input.GetKeyDown(KeyCode.E) && IsGrounded())
        {
            playerMovement.enabled = false;
            axeThrow.enabled = false;
            hiding = true;
            hidable = false;
            buttonERenderer.enabled = false;
            playerRenderer.enabled = false;
            hideCollider.isTrigger = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;

            transform.gameObject.tag = "hidingPlayer";
            transform.gameObject.layer = LayerMask.NameToLayer("hidingPlayer");
            rb.velocity = new Vector3(0, 0, 0);
        }
        else if (hiding == true && Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.enabled = true;
            axeThrow.enabled = true;
            hiding = false;
            hidable = true;
            buttonERenderer.enabled = true;
            playerRenderer.enabled = true;
            hideCollider.isTrigger = false;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            transform.gameObject.tag = "Player";
            transform.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("hidingSpot"))
        {
            buttonERenderer.enabled = true;
            hidable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("hidingSpot"))
        {
            buttonERenderer.enabled = false;
            hidable = false;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(hideCollider.bounds.center, hideCollider.bounds.size, 0f, Vector2.down, .4f, jumpGround);
    }
}
