using UnityEngine;
using System.Collections;

public class ProximityTrigger : MonoBehaviour {

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
