using UnityEngine;
using System.Collections;

public class ApplicationForm : MonoBehaviour {

	[SerializeField]
	private Sprite[] _stateSprites;
	[SerializeField]
	private GameObject _completeText;

	public ProximityTrigger Trigger
	{
		get
		{
			return GetComponent<ProximityTrigger>();
		}
	}

	public void UpdateVisualState(int stage)
	{
		GetComponent<SpriteRenderer>().sprite = _stateSprites[stage];
	}

	public void PlaySubmitAnimation()
	{
		GetComponent<SpriteRenderer>().color = Color.white;
		Camera.main.transform.parent = gameObject.transform;
		GetComponent<Animator>().SetTrigger("gameComplete");
	}

	public void StopCameraTrackingCV()
	{
		Camera.main.transform.parent = gameObject.transform.parent;
		_completeText.SetActive(true);
	}

}
