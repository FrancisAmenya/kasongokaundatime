using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles constantly moving the obstacle to the left
public class Obstacle : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    //When the obstacle appears, make it move to the left
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, 0);
    }
}
