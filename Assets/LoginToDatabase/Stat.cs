using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat{

	[SerializeField]
	public BarScript bar;
	[SerializeField]
	private float maxValue;
	[SerializeField]
	private float currentValue;
	public float CurrentValue {
		get {
			return currentValue;
		}
		set {
			currentValue = Mathf.Clamp(value, 0, MaxValue);
			bar.Value = currentValue;
		}
	}

	public float MaxValue {
		get {
			return maxValue;
		}
		set {
			maxValue = value;
			bar.MaxValue = maxValue;
		}
	}
		
}
