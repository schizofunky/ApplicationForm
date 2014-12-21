using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectivePanel : MonoBehaviour {

	[SerializeField]
	private Text _objectiveText;

	public void UpdateObjective(string objective) 
	{
		_objectiveText.text = objective;
	}
	
	public void ShowObjectivePanel()
	{		
		gameObject.SetActive(true);
	}

	public void HideObjectivePanel()
	{		
		gameObject.SetActive(false);
	}
}
