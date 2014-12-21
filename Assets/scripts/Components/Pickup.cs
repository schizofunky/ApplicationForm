using UnityEngine;
using System.Collections;
using System;

public class Pickup : MonoBehaviour {

	[SerializeField]
	private int _pickupId;

	private Action<int> _onPickupObtained;

	public Action<int> OnPickupObtained
	{
		set
		{
			_onPickupObtained = value;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		_onPickupObtained(_pickupId);
		Destroy(gameObject);
	}
}
