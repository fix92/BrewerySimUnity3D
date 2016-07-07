using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour {

	bool rotateOn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (rotateOn) {
			transform.Rotate(Vector3.up * Time.deltaTime*20);
		}
	}

	public void startRotating() {
		rotateOn = true;
	}
	public void stopRotating() {
		rotateOn = false;
	}

}
