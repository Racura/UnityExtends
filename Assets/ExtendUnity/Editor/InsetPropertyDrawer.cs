using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomPropertyDrawer (typeof (InsetF))]
public class InsetPropertyDrawer  : PropertyDrawer {

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return EditorGUIUtility.singleLineHeight * 4;
	}


	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {

		float LabelWidth = (int)Mathf.Max(52, pos.width * 0.2f);


		SerializedProperty topProp			= prop.FindPropertyRelative ("top");

		SerializedProperty leftProp			= prop.FindPropertyRelative ("left");
		SerializedProperty rightProp		= prop.FindPropertyRelative ("right");

		SerializedProperty bottomProp		= prop.FindPropertyRelative ("bottom");

		EditorGUI.LabelField (
			new Rect(pos.x, pos.y, pos.width, EditorGUIUtility.singleLineHeight),
      		label
		);

		var spacing = Mathf.Min(10, pos.width * 0.1f);
		
		var y = EditorGUIUtility.singleLineHeight + pos.y;
		var x = pos.x + spacing;

		var stepWidth = (pos.width - spacing) / 3;

		var height = EditorGUIUtility.singleLineHeight;

		
		float labelWidthTmp = EditorGUIUtility.labelWidth;
		EditorGUIUtility.labelWidth = LabelWidth;
		
		EditorGUI.PropertyField(new Rect (x + stepWidth * 1, y, stepWidth, height), topProp);
		
		y = EditorGUIUtility.singleLineHeight * 2 + pos.y;

		EditorGUI.PropertyField(new Rect (x + stepWidth * 0, y, stepWidth, height), leftProp);
		EditorGUI.PropertyField(new Rect (x + stepWidth * 2, y, stepWidth, height), rightProp);

		y = EditorGUIUtility.singleLineHeight * 3 + pos.y;

		EditorGUI.PropertyField(new Rect (x + stepWidth * 1, y, stepWidth, height), bottomProp);
		
		EditorGUIUtility.labelWidth = labelWidthTmp;

	}
}
