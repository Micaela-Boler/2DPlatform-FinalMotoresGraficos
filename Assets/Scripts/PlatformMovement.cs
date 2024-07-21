using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] protected Transform[] movementPoints;
    [SerializeField] protected float minDistance;
    [HideInInspector] protected int randomNumber = 0;
    [SerializeField] protected float speed;



    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movementPoints[randomNumber].position, speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, movementPoints[randomNumber].position) <= minDistance)
            randomNumber = Random.Range(0, movementPoints.Length);
    }
}
