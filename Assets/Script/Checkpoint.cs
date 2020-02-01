using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : RaycastController {

	public RespawnManager respawnManager;
    public LayerMask checkMask;
    public float checkCircleSize = 2f;

    // Use this for initialization
    public override void Start () {
		respawnManager = FindObjectOfType<RespawnManager> ();
	}
	

    void Update()
    {
        UpdateRaycastOrigins();
        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        Vector2 rayOrigin = raycastOrigins.center;
        Collider2D collider = Physics2D.OverlapCircle(rayOrigin, checkCircleSize, checkMask);

        if (collider)
        {
            respawnManager.currentCheckpoint = gameObject;
        }
    }

    void OnDrawGizmos()
    {
        Vector2 rayOrigin = raycastOrigins.center;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rayOrigin, checkCircleSize);
    }
}
