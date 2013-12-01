using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	
	[HideInInspector]
	public bool facingRight = true;
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

	public bool grounded = false;			// Whether or not the player is grounded.

	public float moveForce = 365f;
	public float maxSpeed = 3f;
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 365f;			// Amount of force added when the player jumps.

	private Transform groundCheck;			// A position marking where to check if the player is grounded.

//	private float rndWait = 1f;
//	private Animator anim;
	
	void Awake () 
	{
		groundCheck = transform.Find("groundCheck");
		//		anim = GetComponent<Animator> ();
	}
	
	void Start() 
	{
//		StartCoroutine (GiveRandomWait());
	}
	
	void Update () 
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));  

		if(Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}
	}
	void FixedUpdate () 
	{
		/*
		if (Input.GetButton("Sprint")) {
			maxSpeed = 10f;
			anim.SetInteger("Run", 1);
		} else {
			maxSpeed = 3f;
			anim.SetInteger("Run", 0);
		}
		*/
		float h = Input.GetAxis ("Horizontal");
		/*
		if (Mathf.Abs(h) == 0) {
			anim.SetInteger("Wait", (int) rndWait);
		}
		
		anim.SetFloat ("Speed", Mathf.Abs (h));
		*/
		if ( h * rigidbody2D.velocity.x < maxSpeed ) {
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		}
		if ( Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed ) {
			rigidbody2D.velocity = new Vector2 (Mathf.Sign (rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		}
		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) {
			Flip ();
		}

		if(jump)
		{
			// Set the Jump animator trigger parameter.
			//anim.SetTrigger("Jump");
			
			// Play a random jump audio clip.
			int i = Random.Range(0, jumpClips.Length);
			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}

	}
	
	void Flip() 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	/*
	IEnumerator GiveRandomWait()
	{
		while(true)
		{
			rndWait = Random.Range (1, 3);
			yield return new WaitForSeconds(10);
		}
	}
	*/
}
