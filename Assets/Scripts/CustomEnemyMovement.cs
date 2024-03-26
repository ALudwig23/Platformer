using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEnemyMovement : Movement
{
    protected bool FlipDirection = false;

    protected override void HandleInput()
    {
        if (FlipDirection)
        {
            Debug.Log("moving left");
            _inputDirection = Vector2.left;
        }
        else
        {
            Debug.Log("moving right");
            _inputDirection = Vector2.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Boundary"))
            return;

        FlipDirection = !FlipDirection;
    }
}
