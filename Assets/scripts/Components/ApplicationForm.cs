using UnityEngine;
using System.Collections;

public class ApplicationForm : MonoBehaviour {

	public Sprite[] _stateSprites;
	public GameObject _completeText;

	public ProximityTrigger Trigger
	{
		get
		{
			return GetComponent<ProximityTrigger>();
		}
	}

	/*
	 * Changes which image to display for the Application Form
	 */ 
	public void UpdateVisualState(int stage)
	{
		if(stage < _stateSprites.Length)
		{
			GetComponent<SpriteRenderer>().sprite = _stateSprites[stage];
		}
	}

	/*
	 * Play the end sequence of the game.  
	 * Make the sprite white (removes transparency) and trigger the complete animation
	 * Also makes the camera track the object by making it a child object
	 */ 
	public void PlayEndSequence()
	{
		GetComponent<SpriteRenderer>().color = Color.white;
		Camera.main.transform.parent = gameObject.transform;
		GetComponent<Animator>().SetTrigger("gameComplete");
	}

	/*
	 *Stops the camera tracking the object
	 *This function is called from the animation timeline to sync it correctly
	 */ 
	public void StopCameraTrackingCV()
	{
		Camera.main.transform.parent = gameObject.transform.parent;
		_completeText.SetActive(true);
	}

}
