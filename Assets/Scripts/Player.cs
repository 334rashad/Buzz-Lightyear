using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerMoveSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerMoveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * playerMoveSpeed;
        var newXPosition = transform.position.x + deltaX;
        var newYPosition = transform.position.y + deltaY;
        transform.position = new Vector2(newXPosition, newYPosition);
    }
}
