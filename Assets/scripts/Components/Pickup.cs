using UnityEngine;
using System.Collections;
using System;

public class Pickup : MonoBehaviour {

	public int _pickupId;

	private Action<int> _onPickupObtained;

	public Action<int> OnPickupObtained
	{
		set
		{
			_onPickupObtained = value;
		}
	}

	/*
	 * On collison use the Action delegate to signal to our GameController to handle the event
	 * and destroy this game objects instance
	 */ 
	private void OnCollisionEnter2D(Collision2D collision)
	{
		_onPickupObtained(_pickupId);
		Destroy(gameObject);
	}
}
