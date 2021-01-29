using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_At_Player : MonoBehaviour
{	
	public float speed;
	public Moveable player;
	public Transform camera_point;
	public Vector3 initial_point;
	[HideInInspector]
	public Transform player_trans;

	void Start()
	{	player_trans = player.transform;
		initial_point = camera_point.position;
	}

	void Update()
	{	transform.LookAt(player_trans, Vector3.up);
		transform.position = Vector3.Lerp(transform.position, camera_point.position, speed);
	}

	void ResetCamera()
		=> transform.position = camera_point.position = initial_point;
}
