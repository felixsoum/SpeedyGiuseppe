using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	public Transform focus;
	
	void Update () {
		Vector3 nextPosition = transform.position;
		nextPosition.x = focus.position.x;
		transform.position = nextPosition;
	}
}
