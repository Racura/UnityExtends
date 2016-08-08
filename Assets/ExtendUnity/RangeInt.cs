using UnityEngine;
using System.Collections;

[System.Serializable]
public struct RangeInt {

	public int min;
	public int max;

	public RangeInt(int min, int max) {
		this.min = min;
		this.max = max;
	}
	
	public float Clamp(float value){
		
		return Mathf.Clamp(value, min, max);
	}

	public float Lerp(float value){

		return Mathf.Lerp(min, max, value);
	}

	public float LerpInt(float value){

		return Mathf.FloorToInt(Mathf.Lerp(min, max + 1, value));
	}
	
	public int Random(){
		
		return UnityEngine.Random.Range(min, max + 1);
	}

	public bool Inside (float value)
	{
		return value >= min && value <= max;
	}

	public float Difference (float value)
	{
		return Clamp(value) - value;
	}

	
	public static implicit operator RangeInt(int i)
	{
		return new RangeInt(i, i);
	}
	
	
	public static readonly RangeInt Range01 = new RangeInt (0, 1);
}