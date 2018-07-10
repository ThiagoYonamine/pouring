using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private Color tmp;
	// Use this for initialization
	void Start () {
		tmp = this.GetComponent<SpriteRenderer>().color;
		tmp.a = 1f;
		this.GetComponent<SpriteRenderer>().color = tmp;	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		tmp.a -= 0.01f;
		this.GetComponent<SpriteRenderer>().color = tmp;
		if(tmp.a <= 0){
			Destroy(this.gameObject);
		}	
	}
}
