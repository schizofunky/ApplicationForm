using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ConsoleDialogBox : MonoBehaviour {

	public Text _titleTextUI;
	public Text _descriptionTextUI;

	private Action _onClosed;

	/*
	 * Sets up the Console UI with the passed pickup data and enables it
	 * Also stores an Action delegate reference for when the screen is closed
	 */ 
	public void Show(PickupData pickup, Action onClosed)
	{
		_titleTextUI.text = pickup.name;
		_descriptionTextUI.text = pickup.description;
		gameObject.SetActive(true);
		_onClosed = onClosed;
	}

	public void Close()
	{
		gameObject.SetActive(false);
		_onClosed();
	}
}
