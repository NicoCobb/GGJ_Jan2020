using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int rectscale = 5;
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
        Vector2 right = new Vector2(.01f, 0);
        Vector2 left = new Vector2(-.01f, 0);
        Vector3 currentpos = transform.position;
        print(currentpos[0]);
        if (movedirect == 0)
        {
            if (currentpos[0] >= 3.0f)
            {
                print("uh, hello?");
                movedirect = 1;
            }
            else
            moveable.Move(right);
        }
        else if (movedirect == 1)
        {
            if (currentpos[0] <= -3.0f)
            {
                print("What?");
                movedirect = 0; 
            }
            else
            moveable.Move(left);
        }
    }
}
