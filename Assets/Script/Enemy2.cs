using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : RaycastController
{
    public int radius = 5;
    public LayerMask playerMask;
    bool moveRight = false;
    bool recentContact = false;
    CircleMoveable2D moveable;

    // Start is called before the first frame update
    void Start()
    {
        moveable = GetComponent<CircleMoveable2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleDeflect();
    }

    void Move()
    {
        Vector2 right = new Vector2(.01f, 0);
        Vector2 left = new Vector2(-.01f, 0);
        Vector3 currentpos = transform.position;
        if (moveRight)
        {
            if (currentpos[0] >= 37.0f)
                moveRight = false;
            else
                moveable.Move(right);
        }
        else
        {
            if (currentpos[0] <= 25.0f)
                moveRight = true;
            else
                moveable.Move(left);
        }
    }

    void HandleDeflect()
    {
        Vector2 rayOrigin = raycastOrigins.center;
        Collider2D collider = Physics2D.OverlapCircle(rayOrigin, radius, playerMask);

        if (collider && !recentContact)
        {
            Player player = GetComponent<Player>();
            print(player.velocity);
            print(player.velocity);
            player.velocity.x *= -2;
            player.velocity.y *= -2;
            recentContact = true;
        }
        if (recentContact && !collider)
        {
            recentContact = false;
        }

    }
}