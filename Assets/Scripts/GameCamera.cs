using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float cameraSpeed;

    [SerializeField] float YPosition;



    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y + YPosition, -5f), cameraSpeed * Time.deltaTime);
    }
}
