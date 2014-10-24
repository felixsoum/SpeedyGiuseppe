using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {
	
	public static float distanceTraveled;
	private bool touchingPlatform;
	public static int boosts;

	public float maxSpeed = 1f;
	BoxCollider2D boxCollider;
	bool jump = false;
	bool run = false;
	int counter = 60;
	public float gameOverY;

	private Vector3 startPosition;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody2D.isKinematic = true;
		enabled = false;
	}

	void FixedUpdate()
	{
		float move = Input.GetAxis("Horizontal");
		if (move > 0 || move < 0) {
			if(maxSpeed >= 10f)
			{
				if(maxSpeed >= 40f)
				{
					maxSpeed+= 0.001f;
				}
				else
				{
					maxSpeed+= 0.1f;
				}
			}
			else
			{
				print(maxSpeed);
				maxSpeed+=1f;
			}
		} else {
			maxSpeed = 1f;
		}
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
		
		if(counter <= 0)
		{
			jump = false;
		}
		counter--;
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Space) && (jump == false)) 
		{
			rigidbody2D.AddForce(new Vector2(0,500f));
			jump = true;
			counter = 45;
			maxSpeed = 1f;
		} 
		distanceTraveled = transform.localPosition.x;
		
		if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}

	}

	private void GameStart () {
		boosts = 0;
		distanceTraveled = 0f;
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody2D.isKinematic = false;
		enabled = true;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody2D.isKinematic = true;
		enabled = false;
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}
	
	void OnCollisionExit () {
		touchingPlatform = false;
	}

	public static void AddBoost () {
		boosts += 1;
	}
}