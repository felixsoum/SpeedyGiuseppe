using UnityEngine;
using System.Collections;

public class SpeedyCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = this.transform.position;
        pos.z = -40f;
        pos.y = 0f;
        this.transform.position = pos;

        int r = Random.Range(0, 100);
        if (r == 0)
        {
            pos = this.transform.position;
            pos.z = 5f;
            pos.y = 40f;
            this.transform.position = pos;

        }
	}
}
