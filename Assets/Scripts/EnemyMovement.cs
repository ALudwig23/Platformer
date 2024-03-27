using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    public Transform Target;
    public PolygonCollider2D _enemyVision;
    protected bool FlipDirection = false;
    

    protected bool _canSeePlayer = false;

    protected override void HandleInput()
    {
        if (_canSeePlayer == true)
        {
            if (Target == null)
            {
                Target = GameObject.FindGameObjectWithTag("Player").transform;
            }
            if (Target == null)
                return;
        }

        Vector2 targetDirection = Target.position - transform.position;
        targetDirection = targetDirection.normalized;

        _inputDirection = targetDirection;

        if (_canSeePlayer == false)
        {
            if (FlipDirection)
            {
                //Debug.Log("moving left");
                _inputDirection = Vector2.left;
            }
            else
            {
                //Debug.Log("moving right");
                _inputDirection = Vector2.right;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canSeePlayer = true;
            Debug.Log("Can see player");
            accelerator = accelerator * 2;
            return;
        }
        else if((collision.CompareTag("Boundary")))
        {
            Debug.Log("No player in sight");
            _canSeePlayer = false;
            accelerator = 3;
            FlipDirection = !FlipDirection;
        }
    }
}
