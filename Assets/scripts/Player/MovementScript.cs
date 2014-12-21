using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	[SerializeField]
	private float _speed = 4;

	private Vector2 _movement;
	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

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

	public void SetIdleUp()
	{		
		_animator.SetFloat("Vertical",0.01f);
		_animator.SetBool("Idle",true);
	}
	
	private void Update()
	{
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		_animator.SetBool("Idle", (inputY == 0 && inputX == 0));

		_movement = new Vector2(
			_speed * inputX,
			_speed * inputY);

	}
	
	private void FixedUpdate()
	{
		rigidbody2D.velocity = _movement;
		_animator.SetFloat("Horizontal",_movement.x);
		_animator.SetFloat("Vertical",_movement.y);
	}

}
