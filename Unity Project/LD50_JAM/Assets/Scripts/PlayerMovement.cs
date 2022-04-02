using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;

    [SerializeField] float MoveSpeed;    

    // Start is called before the first frame update
    void Start()
    {
        if (rb2d == null)
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
    }


    public void Move(Vector2 movement)
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        rb2d.MovePosition(currentPosition + movement.normalized * MoveSpeed * Time.fixedDeltaTime);
    }
}
