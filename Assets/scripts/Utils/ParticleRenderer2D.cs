using UnityEngine;
using System.Collections;

public class ParticleRenderer2D : MonoBehaviour {

	[SerializeField]
	private string _sortingLayerName;
	[SerializeField]
	private int _sortingOrder;

	private void Awake () 
	{
		var renderer = GetComponent<Renderer>();
		renderer.sortingLayerName = _sortingLayerName;
		renderer.sortingOrder = _sortingOrder;	
	}
}
