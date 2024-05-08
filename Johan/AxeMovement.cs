using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float speedUp = 2f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if(PlayerMovement.facingRight == true)
        {
            rb.velocity = new Vector2(speed, speedUp);
        }
        else if (PlayerMovement.facingRight == false)
        {
            sprite.flipX = true;
            rb.velocity = new Vector2(-speed, speedUp);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }
}
