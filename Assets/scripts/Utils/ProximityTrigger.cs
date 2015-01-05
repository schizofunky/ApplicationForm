using UnityEngine;
using System.Collections;

public class ProximityTrigger : MonoBehaviour {

	/*
	 * This helper behaviour is used by other classes to check and handle collisions
	 * to objects that use this behaviour
	 */
	private bool _withinRange;
	
	public bool WithinRange
	{
		get
		{
			return _withinRange;
		}
		set
		{
			_withinRange = value;
		}
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		WithinRange = true;	
	}
	
	private void OnTriggerExit2D(Collider2D collider)
	{
		WithinRange = false;
	}
}
