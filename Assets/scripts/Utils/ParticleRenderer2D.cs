using UnityEngine;
using System.Collections;

public class ParticleRenderer2D : MonoBehaviour {

	public string _sortingLayerName;
	public int _sortingOrder;
	/*
	 * This is a work around to show particle systems layered with the Unity 2D layers 
	 */
	private void Awake () 
	{
		var renderer = GetComponent<Renderer>();
		renderer.sortingLayerName = _sortingLayerName;
		renderer.sortingOrder = _sortingOrder;	
	}
}
