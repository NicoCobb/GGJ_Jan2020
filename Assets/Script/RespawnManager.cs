using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour {

	public GameObject currentCheckpoint;
	private Player player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.y < -50f) {
			respawnPlayer ();
		}
	}

	public void respawnPlayer() {
		player.transform.position = currentCheckpoint.transform.position;
	}
}
