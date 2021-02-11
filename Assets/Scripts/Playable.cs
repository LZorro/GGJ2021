using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Playable : Moveable
{
	public new GameObject camera;
	public Rigidbody body;
	public float speed;
	public float rotation_speed;
	public float jump_power;

#if EXTERNALCAMERA
	public const float dampener = 1;
#else
	public const float dampener = 3;
#endif

	private Collider floor_collider;
	private float horz, latr, angu;
	private Sprite_Script sprite_script;

    public GameObject nearbyCharacter = null;
	private GameManager gm;

	void Start ()
	{	Init();
		sprite_script = GetComponentInChildren<Sprite_Script>(/*"Jim_Sock"*/);
		gm = GameObject.FindObjectOfType<GameManager>();
	}
	
	void FixedUpdate ()
	{	var direction = camera.transform.TransformDirection(V(horz, 0, latr));
		var vector = speed * V(direction.x, 0, direction.z).normalized;
		body.velocity = V(vector.x, body.velocity.y, vector.z);
		if (horz != 0 || angu != 0)
			transform.Rotate( V(0, 0, (horz/dampener + angu) * rotation_speed) );
	}

	void Update ()
	{	horz = Input.GetAxis("Horizontal");
		latr = Input.GetAxis( "Vertical" );
		angu = Input.GetAxis("RS_h");//Horizontal Right Stick
		if (Input.GetButtonDown("Jump") && On())
		{	Jump();
			sprite_script.Jump();
		}
        if (Input.GetButtonDown("Fire1") && nearbyCharacter != null && !gm.isSlideshowOpen)
        {
            nearbyCharacter.GetComponent<CharacterDialog>().advanceDialog();
        }
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
    }

	
	private void Jump()
		=> phys.AddForce(transform.forward * jump_power);


	private IEnumerator OnCollisionEnter(Collision collision)
	{	GameObject hit = collision.gameObject;
		yield return null;
	}

	private IEnumerator OnTriggerEnter(Collider collider)
	{	GameObject hit = collider.gameObject;
        if (hit.tag.Equals("Character"))
        {
            nearbyCharacter = hit;
            hit.GetComponent<CharacterDialog>().enablePrompt(true);
        }
		yield return null;
	}

    private void OnTriggerExit(Collider other)
    {
        GameObject hit = other.gameObject;
        if (hit.tag.Equals("Character"))
        {
            hit.GetComponent<CharacterDialog>().enablePrompt(false);
            nearbyCharacter = null;
        }
    }
}
