using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private Vector3 Veloctiy;
	private Vector3 Acceleration;
	private Vector3 Steering;
	private float acceleration = 2.0f;
	private float max_Velocity = 10.0f;
	private float rotate_Angle;

	void Start () {
		Veloctiy = new Vector3 (Random.Range (-70.0f, -25.0f),0.0f, Random.Range (-70.0f, -25.0f));
		rotate_Angle = Random.Range (-2.0f, 2.0f);
	}

	void Update () {
		AI_Run ();

		if (!this.gameObject.renderer.isVisible) {
			transform.position = new Vector3( 0.0f, 0.0f, 0.0f ); 
		}
	}

	void AI_Run(){
		transform.eulerAngles += new Vector3( 0.0f, rotate_Angle, 0.0f );
		transform.position += transform.forward * max_Velocity * Time.deltaTime;
	}
}
