using UnityEngine;
using System.Collections;

[System.Serializable]
public struct RangeF {

	public float min;
	public float max;

	public RangeF(float min, float max) {
		this.min = min;
		this.max = max;
	}
	
	public float Clamp(float value){
		
		return Mathf.Clamp(value, min, max);
	}
	
	public float Lerp(float value){
		
		return Mathf.Lerp(min, max, value);
	}

	public float InverseLerp (float value)
	{
		return (value - min) / (max - min);
	}
	
	public float Random(){
		
		return UnityEngine.Random.Range(min, max);
	}

	public int RandomInt ()
	{
		return (int)Random();
	}

	public bool Inside (float value)
	{
		return value >= min && value <= max;
	}

	public float Difference (float value)
	{
		return Clamp(value) - value;
	}

	
	public static implicit operator RangeF(int i)
	{
		return new RangeF(i, i);
	}

	public static implicit operator RangeF(float i)
	{
		return new RangeF(i, i);
	}
	
	public static implicit operator RangeF(RangeInt i)
	{
		return new RangeF(i.min, i.max);
	}

	public static explicit operator RangeInt(RangeF i)
	{
		return new RangeInt((int)i.min, (int)i.max);
	}
	
	public static readonly RangeF Range01 = new RangeF (0, 1);
}