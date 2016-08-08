using UnityEngine;
using System.Collections;

public static partial class ExtendsUtil {

	
	public static IEnumerator WaitForAnimation(this Animator animator, int layerIndex)
	{
		yield return null;
		
		var info = animator.GetCurrentAnimatorStateInfo(layerIndex);
		var next = animator.GetNextAnimatorStateInfo(layerIndex);
		
		while (info.normalizedTime < 1 || next.normalizedTime != 0) {
			yield return null;
			info = animator.GetCurrentAnimatorStateInfo(layerIndex);
			next = animator.GetNextAnimatorStateInfo(layerIndex);
		}
	}



	public static bool IsAnimating(this Animator animator, int layerIndex)
	{
		var info = animator.GetCurrentAnimatorStateInfo(layerIndex);
		var next = animator.GetNextAnimatorStateInfo(layerIndex);

		return info.normalizedTime < 1 || next.normalizedTime != 0;
	}

	public static void SetLayer(this GameObject gameobject, int layer, bool setChildren)
	{
		gameobject.layer = layer;

		if(setChildren) {
			for(int i = gameobject.transform.childCount - 1; i >= 0; --i) {
				SetLayer(gameobject.transform.GetChild(i).gameObject, layer, setChildren);
			}
		}
	}


    public static YieldInstruction WaitForUnscaledSeconds (this MonoBehaviour component, float time)
	{
		return component.StartCoroutine(
			WaitForUnscaledSecondsCorroutine(time)
		);
	}
	
	public static IEnumerator WaitForUnscaledSecondsCorroutine (float time)
	{
		float t = Time.unscaledTime + time;
		while(t > Time.unscaledTime) {
			yield return null;
		}
	}

	public static Vector3 WorldToCanvasPoint(this Canvas canvas, Vector3 worldPosition)
	{
		var camera = canvas.worldCamera;
		if (camera == null) camera = Camera.main;
		
		var viewport_position = camera.WorldToViewportPoint(worldPosition);
		var canvas_rect = canvas.GetComponent<RectTransform>();
		
		return new Vector2((viewport_position.x * canvas_rect.sizeDelta.x) - (canvas_rect.sizeDelta.x * 0.5f),
		                   (viewport_position.y * canvas_rect.sizeDelta.y) - (canvas_rect.sizeDelta.y * 0.5f));
	}


	
	public static T GetIComponentInChildren<T>(this Component component)
		where T : class
	{
		return component.GetComponentInChildren(typeof(T)) as T;
    }

    public static T GetIComponentInParent<T>(this Component component)
        where T : class
    {
        return component.GetComponentInParent(typeof(T)) as T;
    }

    public static T GetIComponent<T>(this Component component)
		where T : class
	{
		return component.GetComponent(typeof(T)) as T;
    }

    public static T[] GetIComponentsInChildren<T>(this Component component, bool includeInactive = false)
        where T : class
    {
        var c = component.GetComponentsInChildren(typeof(T), includeInactive);
        var output = new T[c.Length];

        for (int i = 0; i < output.Length; ++i)
            output[i] = c[i] as T;

        return output;
    }

    public static T[] GetIComponentsInParent<T>(this Component component, bool includeInactive = false)
        where T : class
    {
        var c = component.GetComponentsInParent(typeof(T), includeInactive);
        var output = new T[c.Length];

        for (int i = 0; i < output.Length; ++i)
            output[i] = c[i] as T;

        return output;
    }

    public static T[] GetIComponents<T>(this Component component)
		where T : class
	{
		var c = component.GetComponents(typeof(T));
		var output = new T[c.Length];

		for(int i = 0; i < c.Length; ++i)
			output[i] = c[i] as T;

		return output;
	}



	
	
	public static Vector2 RotateBy(this Vector2 bas, Vector2 right){
		return new Vector2(
			bas.x * right.x - bas.y * right.y, 
			bas.y * right.x + bas.x * right.y
		);
	}
	
	
	public static Color MultiplyAlpha(this Color color, float alpha){
		return new Color(color.r, color.g, color.b, Mathf.Clamp01(alpha * color.a));
	}
	
	public static T GetOrAddComponent<T>(this Component componet)
		where T : Component
	{
		return componet.GetOrAddComponent<T>(HideFlags.None);
	}

	public static T GetOrAddComponent<T>(this Component componet, HideFlags addFlags)
		where T : Component
		{
		var t = componet.GetComponent<T>();

		if(t == null)	{
			t = componet.gameObject.AddComponent<T>();
			t.hideFlags = addFlags;
		}
		return t;
	}


	public static float GetLength (this AnimationCurve curve) {
	
		var output = 0f;

		for(int i = curve.length - 1; i >= 0; --i)
			output = Mathf.Max(output, curve[i].time);


		return output;
	}

}