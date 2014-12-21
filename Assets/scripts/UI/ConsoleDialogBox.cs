using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ConsoleDialogBox : MonoBehaviour {

	[SerializeField]
	private Text _titleTextUI;
	[SerializeField]
	private Text _descriptionTextUI;

	private Action _onClosed;

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
