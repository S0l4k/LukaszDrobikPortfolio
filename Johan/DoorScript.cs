using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _keyObject;
    [SerializeField] private Collider2D _wall;
    [SerializeField] private Sprite _s2;
    
    [Header("Variables")]
    private bool _playerColliding;
    private bool _opened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _keyObject.tag == "Collected" && _opened == false)
        {
            GetComponent<SpriteRenderer>().sprite = _s2;
            _wall.enabled = false;
            Debug.Log("Enter");
        }
    }
}
