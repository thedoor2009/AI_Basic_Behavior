using UnityEngine;
using System.Collections;

public class GameRender : MonoBehaviour {
	//public Color c1 = Color.yellow;
	//public Color c2 = Color.red;
	//public Material mat;
	public int lengthOfLineRenderer = 2;

	void Start() {
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);

	}
	void Update() {

		this.GetComponent<LineRenderer> ().SetPosition (0, this.transform.position);
		this.GetComponent<LineRenderer> ().SetPosition (1, this.transform.position + this.transform.forward.normalized * 3.0f );
	}
}
