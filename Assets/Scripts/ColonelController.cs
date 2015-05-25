using UnityEngine;
using System.Collections;

public class ColonelController : MonoBehaviour {

	Rigidbody2D rb;
	Animator anim;
	public float maxSpeed = 10;
	public float jumpForce = 3000;
	
	bool isGrounded {
		get { 
			return rb.velocity.y == 0; //пока так
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	void Update ()
	{
		//		if (isGrounded && Input.GetKeyDown (KeyCode.W)) {
		//			rb.AddForce (new Vector2 (0, jumpForce));
		//		}
	}
	
	void FixedUpdate ()
	{

		rb.velocity = new Vector2 (1 * maxSpeed, rb.velocity.y);

	}
	
	void LateUpdate ()
	{
		SetDirection();
		SetAnimation();
	}
	
	void SetDirection()
	{
		if(rb.velocity.x == 0)
			return;
		var direction = rb.velocity.x < 0 ? -1 : 1;
		var scale = transform.localScale;
		scale.x = Mathf.Abs (scale.x) * direction;
		transform.localScale = scale;
	}
	
	void SetAnimation()
	{
		anim.SetBool("IsRun", rb.velocity.x != 0);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{

	}
}
