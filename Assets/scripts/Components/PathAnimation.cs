using UnityEngine;
using System.Collections;

public class PathAnimation : MonoBehaviour {

	[SerializeField]
	private GameObject _smallTile;
	[SerializeField]
	private GameObject _mediumTile;
	[SerializeField]
	private GameObject _largeTile;
	[SerializeField]
	private BoxCollider2D _pathBlocker;

	private void Start()
	{
		_smallTile.SetActive(false);
		_mediumTile.SetActive(false);
		_largeTile.SetActive(false);
	}

	public void PlayUnlockAnimation()
	{
		_pathBlocker.enabled = false;
		StartCoroutine(UnlockCoroutine());
	}

	private IEnumerator UnlockCoroutine()
	{		
		_smallTile.SetActive(true);
		_smallTile.GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(0.5f);

		_mediumTile.SetActive(true);
		_mediumTile.GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(0.5f);

		_largeTile.SetActive(true);
		_largeTile.GetComponent<AudioSource>().Play();
				
		yield return new WaitForSeconds(0.5f);
	}
}
