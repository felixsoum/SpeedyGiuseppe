using UnityEngine;
using System.Collections;

public class Runner : MonoBehaviour {

	public Transform groundPoint;
	//public Animation animation;
	private bool touchingPlatform;
	public static float distanceTraveled;
	public static int boosts;
	public float gameOverY;
	public float jumpPower;
	public float movementPower;
	//public float maxSpeed = 1f;

	BoxCollider2D boxCollider;
	Animator animator;
	bool jump = false;
	bool run = false;
	bool facingRight = true;
	bool isGrounded = false;
	float move;
	int counter = 60;

	private Vector3 startPosition;

	float acceleration = 1.0f;
	float maxSpeed = 20.0f;

	void Awake(){
		animator = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider2D> ();
	}

	void Start () {
		movementPower = 500f;
		jumpPower = 500;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		renderer.enabled = false;
		rigidbody2D.isKinematic = true;
		enabled = false;
	}

	void FixedUpdate()
	{
//		float move = Input.GetAxis("Horizontal");
//		if (move > 0 || move < 0) {
//			if(maxSpeed >= 10f)
//			{
//				if(maxSpeed >= 40f)
//				{
//					maxSpeed+= 0.001f;
//				}
//				else
//				{
//					maxSpeed+= 0.1f;
//				}
//			}
//			else
//			{
//				print(maxSpeed);
//				maxSpeed+=1f;
//			}
//		} else {
//			maxSpeed = 1f;
//		}
//		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
//		
//		if(counter <= 0)
//		{
//			jump = false;
//		}
//		counter--;
		isGrounded = Physics2D.OverlapPoint (groundPoint.position);
		animator.SetBool ("IsGrounded", isGrounded == true);
		move = Input.GetAxis ("Horizontal");

		animator.SetFloat ("Speed", Mathf.Abs (move));
		//Uncomment/Comment in the following line to toggle control in the air
		if(Input.GetAxis ("Horizontal") != 0 /*&& touchingPlatform == true*/) {
			//might need to modify this
			rigidbody2D.AddForce(new Vector2(move, 0) * movementPower * Time.deltaTime);	 

			//animation["speedy_g_run"].speed = 2.0f;
		}

		if((move > 0 && !facingRight) || (move < 0 && facingRight)){
			Flip();
		}
	}
	
	void Update()
	{

		//Horizontal Movement only when grounded


		//Jump only when Grounded
		if (Input.GetKeyDown (KeyCode.Space) && (touchingPlatform == true) && isGrounded) 
		{
			animator.SetBool("IsGrounded", false);
			rigidbody2D.AddForce(new Vector2(0,jumpPower));
			touchingPlatform = false;
		} 
		//Allow for small jumps if spacebar is released early
		else if(Input.GetKeyUp (KeyCode.Space) && rigidbody2D.velocity.y > 0){
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);

		}


		distanceTraveled = transform.localPosition.x;

		//this line fucks shit up for some reason

		
		if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}
	
	}

	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 nextScale = transform.localScale;
		nextScale.x *= -1f; 
		transform.localScale = nextScale;	
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

	void OnCollisionEnter2D (Collision2D col) {
		if(col.gameObject.tag == "Terrain"){
		touchingPlatform = true;
		}
	}
	
	void OnCollisionExit2D (Collision2D col) {
		if(col.gameObject.tag == "Terrain"){
			touchingPlatform = false;
		}
	}


	public static void AddBoost () {
		boosts += 1;
	}
}