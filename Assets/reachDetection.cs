using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class reachDetection : MonoBehaviour
{

    public BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        Player player = transform.GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
