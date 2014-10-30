using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.name == "Runner") {
			rigidbody2D.AddForce(Vector2.right * c.gameObject.rigidbody2D.velocity.x);
        }
	}
}
