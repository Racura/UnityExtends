using UnityEngine;
using UnityEditor;


[CustomPropertyDrawer (typeof (SelectionAttribute))]
public class SelectionDrawer : PropertyDrawer {

	// Draw the property inside the given rect
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {

		// First get the attribute since it contains the range for the slider
		var band = attribute as SelectionAttribute;
		var values = band.Values;

		if(values == null || values.Length == 0) {
			EditorGUI.LabelField (position, label.text, "Band must have aleast 1 value.");
			return;
		}

		if (property.propertyType == SerializedPropertyType.Integer) {

			var currentValue = property.intValue;
			var index = 0;
			var dif = Mathf.Abs(currentValue - values[index]);

			var str = new GUIContent[values.Length];
			for(int i = 0; i < values.Length; ++i) {
				str[i] = new GUIContent(values[i].ToString());

				var tmp = Mathf.Abs(currentValue - values[i]);

				if(tmp < dif) {
					dif = tmp;
					index = i;
				}
			}
			index = EditorGUI.Popup (position, label, index, str);

			var newValue = values[index];

			if (newValue != currentValue) {
				property.intValue = (int)newValue;
			}

			return;
		}

		if (property.propertyType == SerializedPropertyType.Float) {

			var currentValue = property.floatValue;
			var index = 0;
			var dif = Mathf.Abs(currentValue - values[index]);

			var str = new GUIContent[values.Length];
			for(int i = 0; i < values.Length; ++i) {
				str[i] = new GUIContent(values[i].ToString());

				var tmp = Mathf.Abs(currentValue - values[i]);

				if(tmp < dif) {
					dif = tmp;
					index = i;
				}
			}
			index = EditorGUI.Popup (position, label, index, str);

			var newValue = values[index];

			if (!Mathf.Approximately(newValue, currentValue)) {
				property.floatValue = newValue;
			}

			return;
		}


		EditorGUI.LabelField (position, label.text, "Band be a Int or a Float.");
	}
}