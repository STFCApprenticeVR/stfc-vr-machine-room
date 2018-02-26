using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinReset : MonoBehaviour {
	public GameObject text;

	void start() {
		text.SetActive (false);
	}

	public void reset() {
		SceneManager.LoadScene (0);
	}

	public void win() {
		StartCoroutine (winCoRo());
	}

	private IEnumerator winCoRo() {
		text.SetActive (true);
		yield return new WaitForSeconds(5f);
		reset ();
	}
}
