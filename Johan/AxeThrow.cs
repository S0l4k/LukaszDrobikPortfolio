using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private HidingScript hidingScript;
    [SerializeField] private GameObject axePrefab;
    [SerializeField] private GameObject archObject;
    [SerializeField] private Transform firePoint;
    private Animator anim;
    private new BoxCollider2D collider;

    private Rigidbody2D rb;
    private SpriteRenderer archRenderer;

    [SerializeField] private LayerMask jumpGround;
    // Start is called before the first frame update

    private void Awake()
    {
        archRenderer = archObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        archRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && IsGrounded())
        {
            anim.SetTrigger("Aiming");
            archRenderer.enabled = true;
            playerMovement.enabled = false;
            hidingScript.enabled = false;
            rb.velocity = new Vector3(0, 0, 0);

        }
        if(Input.GetKeyUp(KeyCode.Q) && IsGrounded())
        {
            anim.SetTrigger("Throwing");
            archRenderer.enabled = false;
            playerMovement.enabled = true;
            hidingScript.enabled = true;
            Instantiate(axePrefab, firePoint.position, Quaternion.identity);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .4f, jumpGround);
    }
}
