using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : RaycastController
{
    public int radius = 5;
    public int movedirect = 0;
    CircleMoveable2D moveable;
    CircleVelocity test;

    // Start is called before the first frame update
    void Start()
    {
        moveable = GetComponent<CircleMoveable2D>();
        test = GetComponent<CircleVelocity>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleDeflect();
    }

    void Move() {
        Vector2 right = new Vector2(.01f, 0);
        Vector2 left = new Vector2(-.01f, 0);
        Vector3 currentpos = transform.position;
        if (movedirect == 0)
        {
            if (currentpos[0] >= 3.0f)
                movedirect = 1;
            else
                moveable.Move(right);
        }
        else if (movedirect == 1)
        {
            if (currentpos[0] <= -3.0f)
                movedirect = 0;
            else
                moveable.Move(left);
        }
    }

    void HandleDeflect() {
        Vector2 rayOrigin = raycastOrigins.center;
        Collider2D collider = Physics2D.OverlapCircle(rayOrigin, radius);

        if (collider) {
            Player player = GetComponentInParent<Player>();
            player.velocity.x *= -2;
            player.velocity.y *= -2;
        }

    }
}