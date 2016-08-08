using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer (typeof (RangeInt))]
[CustomPropertyDrawer (typeof (RangeF))]
public class RangePropertyDrawer : PropertyDrawer {

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return EditorGUIUtility.singleLineHeight * 2;
	}


	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {

		
		float LabelWidth = (int)Mathf.Max(52, pos.width * 0.2f);

		SerializedProperty minProp = prop.FindPropertyRelative ("min");
		SerializedProperty maxProp = prop.FindPropertyRelative ("max");

		EditorGUI.LabelField (
			new Rect(pos.x, pos.y, pos.width, EditorGUIUtility.singleLineHeight),
      		label
		);
		
		var y = EditorGUIUtility.singleLineHeight + pos.y;
		var x = pos.x + 20;
		var totalWidth = pos.width - 20;
		var width = totalWidth * 0.5f;
		var height = EditorGUIUtility.singleLineHeight;

		
		
		float labelWidthTmp = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = LabelWidth;
		
		EditorGUI.PropertyField(new Rect (x + totalWidth * 0.5f - width, y, width, height), minProp);
		EditorGUI.PropertyField(new Rect (x + totalWidth - width, y, width, height), maxProp);
		
		EditorGUIUtility.labelWidth = labelWidthTmp;
	}
}
