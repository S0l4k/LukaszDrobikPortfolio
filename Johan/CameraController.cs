using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float cameraDistanceX = 2f;
    [SerializeField] private float cameraDistanceY = 2f;

    void Update()
    {
        if (PlayerMovement.facingRight == true)
        {
            transform.position = new Vector2(player.transform.position.x + cameraDistanceX, player.transform.position.y + cameraDistanceY);
        }
        else if (PlayerMovement.facingRight == false)
        {
            transform.position = new Vector2(player.transform.position.x - cameraDistanceX, player.transform.position.y + cameraDistanceY);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, player.transform.position.y + cameraDistanceY);
        }
    }
}
