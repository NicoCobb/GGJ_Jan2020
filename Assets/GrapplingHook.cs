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
    }
    // Update is called once per frame
    void Update()
    {
        //move the actual tongue tip
        transform.position += new Vector3(2,2,0);

        //also collision stuff

       lr.positionCount = 2;
       //set start point to player transform origin
       //TODO: Set grappleOrigin and use instead
       lr.SetPosition(0,p.transform.position);
       //set endpoint to start + toClickLocation * grappleShotSpeed? 
       lr.SetPosition(1, transform.position);
    }
}
