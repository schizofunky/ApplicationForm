using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PromptPanel : MonoBehaviour {

	public Text _promptTextUI;

	/*
	 * Set the prompt text and enable the object
	 */ 
	public void Show(string promptMessage)
	{
		_promptTextUI.text = promptMessage;
		gameObject.SetActive(true);
	}
	/*
	 * Set the prompt text and enable the object but auto hide it after specified time
	 */
	public void Show(string promptMessage,int durationInSeconds)
	{
		Show(promptMessage);
		StartCoroutine(HidePromptAfterSeconds(durationInSeconds));
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	private IEnumerator HidePromptAfterSeconds(int seconds)
	{
		yield return new WaitForSeconds(seconds);
		Hide();
	}
}
