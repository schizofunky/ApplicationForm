using UnityEngine;
using System.Collections;

public class PathAnimation : MonoBehaviour {

	public GameObject _smallTile;
	public GameObject _mediumTile;
	public GameObject _largeTile;
	public BoxCollider2D _pathBlocker;

	/*
	 * Ensure all path elements are disabled to start
	 */ 
	private void Start()
	{
		_smallTile.SetActive(false);
		_mediumTile.SetActive(false);
		_largeTile.SetActive(false);
	}

	/*
	 * Disables the collider blocking movement
	 * Plays the unlock animation via corutine
	 */
	public void PlayUnlockAnimation()
	{
		_pathBlocker.enabled = false;
		StartCoroutine(UnlockCoroutine());
	}

	/*
	 * A coroutine to unlock the path stone by stone, with slight pauses for audio effect
	 */ 
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
