using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    //palyer reference
    Player p;
    float grappleShotVelocity;
    Vector2 mouseClickLoc;

    LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }


    public void AimTongue(Vector2 mouseClick, Player pl, float shootVel) {

        mouseClickLoc = mouseClick;
        p = pl;
        grappleShotVelocity = shootVel;

        Vector2 toClickLocation = mouseClick - (Vector2)p.transform.position;
        toClickLocation.Normalize();

    }
    // Update is called once per frame
    void Update()
    {
        Vector3 setPosOne = transform.position;
        if ((Vector2)transform.position == mouseClickLoc) {
 
        }
        
        //also collision stuff
        transform.position += (Vector3)mouseClickLoc*grappleShotVelocity;

        lr.positionCount = 2;
        //set start point to player transform origin
        //TODO: Set grappleOrigin and use instead
        lr.SetPosition(0,p.transform.position);
        //set endpoint to start + toClickLocation * grappleShotSpeed? 
        lr.SetPosition(1, transform.position);
    }
}
