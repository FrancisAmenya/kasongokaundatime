using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase se encarga de mover constantemente el obstaculo a la izquierda
public class Obstaculo : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    //Cuando el obstaculo aparece hacemos que se mueva hacia la izquierda
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, 0);
    }
}
