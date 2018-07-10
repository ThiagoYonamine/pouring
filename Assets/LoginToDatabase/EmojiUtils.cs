using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiUtils : MonoBehaviour {

    private int happy = Animator.StringToHash("happy");
    private Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
		this.gameObject.SetActive(false);
	}
	public void play(int i){
		this.gameObject.SetActive(true);
		this.transform.position = new Vector2 (Random.Range(-1.9f, 2.16f),Random.Range(1.94f, 2.94f));
		switch(i){
			case 1:
				anim.Play(happy);
				break;
			default:
				break;
		}
	}

	public void disable(){
		this.gameObject.SetActive(false);
	}
}
