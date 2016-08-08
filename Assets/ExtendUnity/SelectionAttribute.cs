using UnityEngine;


public class SelectionAttribute : PropertyAttribute {

	float[] values;
	public float[] Values { get { return values; } }

	public SelectionAttribute (params int[] values) {
		var v = new float[values.Length]; 
		for(int i = 0; i < values.Length; ++i)
			v[i] = values[i];

		this.values = v;
	}
	public SelectionAttribute (params float[] values) {
		this.values = values;
	}

	public override bool Match (object obj) {
		return typeof(int).IsInstanceOfType(obj)
			|| typeof(float).IsInstanceOfType(obj);
	}
}