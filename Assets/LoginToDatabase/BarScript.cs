using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	private float fillAmount;

	[SerializeField]
	private Image content;

	public float MaxValue{ get; set;}

	public float Value{
		set{
			fillAmount = convertFillAmount (value, MaxValue);
		}
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar ();	
	}

	private void HandleBar(){

		if (content.fillAmount != fillAmount) {
			content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime*3);
		}
	}

	private float convertFillAmount(float currentLife, float totalLife){
		return currentLife / totalLife;
	}
}
