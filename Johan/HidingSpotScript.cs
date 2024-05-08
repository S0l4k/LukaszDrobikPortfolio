using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotScript : MonoBehaviour
{
    [SerializeField] Sprite s1, s2;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (HidingScript.hiding == true && other.gameObject.CompareTag("hidingPlayer"))
        {
            spriteRenderer.sprite = s2;

        }

        if (HidingScript.hiding == false)
        {
            spriteRenderer.sprite = s1;

        }
    }
}