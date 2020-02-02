using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    //palyer reference
    Player p;
    float grappleShotVelocity;
    Vector2 mouseClickLoc;
    Vector2 mouseClickDiff;
    bool reachedMax;
    LineRenderer lr;
    Vector2 toClickLocation;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        print(transform.position);
    }

     public void AimTongue(Vector2 mouseClick, Player pl, float shootVel) {
        //take input data
        mouseClickLoc = mouseClick;
        p = pl;
        grappleShotVelocity = shootVel;

        //create vector pointing towards mouse click location
        toClickLocation = mouseClick - (Vector2)p.transform.position;
        toClickLocation.Normalize();

        //absolute value of the difference between start position and where the player clicked
        //note: we don't actually use this probably oops
        mouseClickDiff = new Vector2(Mathf.Abs(mouseClickLoc.x - p.transform.position.x), Mathf.Abs(mouseClickLoc.y - p.transform.position.y));
        p.setSwing(mouseClick);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentClickDiff = new Vector2(Mathf.Abs(mouseClickLoc.x - transform.position.x), Mathf.Abs(mouseClickLoc.y - transform.position.y));

        Vector3 setPosOne = transform.position;
        //if we've passed the mouse point, stop moving it
        if (currentClickDiff.x <= 1.5 && currentClickDiff.y <= 1.5) {
            transform.position = mouseClickLoc;
            reachedMax = true;
        }
        
        //also collision stuff
        if(!reachedMax) {
            transform.position += (Vector3)toClickLocation*grappleShotVelocity;
            // print("moving" + transform.position);
        }

        lr.positionCount = 2;
        //set start point to player transform origin
        //TODO: Set grappleOrigin and use instead
        lr.SetPosition(0, p.transform.position);
        //set endpoint to start + toClickLocation * grappleShotSpeed? 
        lr.SetPosition(1, setPosOne);
    }
}
