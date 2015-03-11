using UnityEngine;
using System.Collections;

public class ObstacleTrigger : MonoBehaviour {

	public float radius;
	public bool hit = false;
	void Start () {

	}

	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			renderer.material.color = Color.red;
			hit = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			renderer.material.color = Color.white;
			hit = false;
		}
	}
}
