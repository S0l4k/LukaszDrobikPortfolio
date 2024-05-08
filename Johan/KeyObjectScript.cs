using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObjectScript : MonoBehaviour
{

    [SerializeField] private Sprite _changedSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Update()
    {
        if(tag == "Collected")
        {
            _spriteRenderer.sprite = _changedSprite;
        }
    }
}
