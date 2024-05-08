using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAndMoonScript : MonoBehaviour
{
    public bool isClouded = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cloud"))
        {
            isClouded = true;
            Debug.Log("helow");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cloud"))
        {
            isClouded = false;
            Debug.Log("helow2");
        }
    }
}
