using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Public_Const : MonoBehaviour
{
	public static Public_Const pc;
	public static float Contactoffset => Physics.defaultContactOffset*1.5f;

	void Awake ()
	{	if (null == pc)
		{	DontDestroyOnLoad(gameObject);
			pc = this;
		} else if (pc != this) Destroy(gameObject);
	}

	public static float Pathag(params float[] values)
	{	if (values.Length == 1) return Mathf.Abs(values[0]);//		Note: make sure this is resolved at compile time.
		double accum = 0.0;
		foreach(float value in values) accum += value*value;
		return (float) System.Math.Sqrt(accum);
	}

	public void Update()
	{
		//quits the game.
		if (Input.GetKey(KeyCode.Q)) Application.Quit();
		if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}

	#region Pausing
	static float backup_timeScale, backup_maxDelta;
		private IEnumerator coroutine;
		public static bool Suspended { get{ return 0.0f == Time.timeScale; } }
		public static bool ispaused;

		private void Start(){ pc.StartCoroutine("Pausing"); }

		//Actually checks when to unpause
		private IEnumerator Pausing() { while(true)
		{	if (Input.GetButtonDown("Pause"))
			{	if (ispaused)			{ Play();	ispaused = false; }
				else if (!Suspended)	{ Stop();	ispaused = true ; }
			} yield return null;
		}}

		public static void Play()
		{	Time.timeScale = backup_timeScale;
			Time.maximumDeltaTime = backup_maxDelta;
		}

		public static void Stop()
		{	backup_timeScale = Time.timeScale;
			backup_maxDelta = Time.maximumDeltaTime;
			Time.timeScale = 0.0f;
			Time.maximumDeltaTime = 0.0f;
		}
	#endregion
}
