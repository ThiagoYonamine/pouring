using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	bool ispressed = false;
	
	[SerializeField]
	private Image content;

	private float fill;
	private bool reset;

	public UnityEvent onLongClick;
	
	void Start () {
		fill = 0;
		reset = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(fill >= 1){
			fill = 0;
			reset = true;
			onLongClick.Invoke();
		}
		
		if(ispressed && !reset){
			fill += 0.01f;
			
		}
		else{
			fill = 0;
		}
		content.fillAmount = fill;
	}
	public void OnPointerDown(PointerEventData eventData) {
		ispressed = true;
		reset = false;
	}
	
	public void OnPointerUp(PointerEventData eventData) {
		ispressed = false;
	}
}
