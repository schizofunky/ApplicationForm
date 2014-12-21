using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PromptPanel : MonoBehaviour {

	[SerializeField]
	private Text _promptTextUI;

	public void Show(string promptMessage)
	{
		_promptTextUI.text = promptMessage;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
