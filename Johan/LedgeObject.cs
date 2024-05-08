using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeObject : MonoBehaviour
{
    [SerializeField] private Sprite _fallenSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D _collider;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("axe"))
        {
            Destroy(other.gameObject);
            spriteRenderer.sprite = _fallenSprite;
            _collider.enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
