using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public float _speed = 4;

	private Vector2 _movement;
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	/*
	 * Stop all movement immediately and disable this script
	 */ 
	public void DisableMovement()
	{
		_animator.SetFloat("Horizontal",0);
		_animator.SetFloat("Vertical",0);
		_animator.SetBool("Idle",true);
		_movement = Vector2.zero;
		this.enabled = false;
	}
	
	public void EnableMovement()
	{
		this.enabled = true;
	}

	public void ShowExamineAnimation()
	{		
		_animator.SetTrigger("Examine");
	}

	/*
	 * USes the input axis to calculate movement, and sets idle if necessary
	 */ 
	private void Update()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		_animator.SetBool("Idle", (inputY == 0 && inputX == 0));

		_movement = new Vector2(
			_speed * inputX,
			_speed * inputY);

	}
	/*
	 * The fixed update is used to animate the movement using the pre claculated values
	 */ 
	private void FixedUpdate()
	{
		rigidbody2D.velocity = _movement;
		_animator.SetFloat("Horizontal",_movement.x);
		_animator.SetFloat("Vertical",_movement.y);
	}

}
