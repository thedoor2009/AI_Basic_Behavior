using UnityEngine;
using System.Collections;

public class ResetTrigger : MonoBehaviour {

	public static bool reset = false;

	void Start () {
	
	}

	void Update () {
	
	}

	void OnTriggerEnter( Collider other ){
		//Debug.Log ("Reset!!!");
		reset = true;
	}
}
