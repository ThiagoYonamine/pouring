using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtils : MonoBehaviour {
	public void gotoScene(string name){
		SceneManager.LoadScene(name, LoadSceneMode.Single);
	}
}
