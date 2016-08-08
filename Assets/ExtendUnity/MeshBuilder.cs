using UnityEngine;
using System.Collections;

public abstract class MeshBuilder : MonoBehaviour {

	[SerializeField] Material material;
	public Material SharedMaterial { 
		get { return material; } 
		set { 
			if(material != value) {
				material = value; 
				realMaterial = null;
				OnMaterialChanged();
			}
		} 
	}
	private Material realMaterial;
	public Material Material { 
		get { 
			if(realMaterial == null && material != null) {
				realMaterial = new Material(material) { hideFlags = HideFlags.DontSave };
			}

			return realMaterial; 
		} 
	}

	bool rebuild;
	Coroutine rebuildCoroutine;

	Mesh mesh1, mesh2;
	bool meshFlip;

	Mesh activeMesh;


	protected virtual void Awake () {
		if(material != null)
			OnMaterialChanged();
	}
	
	
	
	protected virtual void OnMaterialChanged ()
	{

	}

	protected virtual void OnEnable () {
		MarkForRebuild();
	}

	protected virtual void OnDisable () {
	}

	public virtual Mesh GetMesh(bool unique) {
	
		if(unique) {
			meshFlip = !meshFlip;
		}
		
		if(!Application.isPlaying) {

			if(mesh1 == null) {
				mesh1 = new Mesh() {
					hideFlags = HideFlags.DontSave,
					name		= string.Format("Debug mesh '{0}'", gameObject.name)
				};
			}

			return mesh1;
		} else {

			if(meshFlip) {

				if(mesh1 == null) {
					mesh1 = new Mesh() {
						hideFlags = HideFlags.DontSave,
						name		= string.Format("Mesh 1 '{0}'", gameObject.name)
					};
				}

				return mesh1;
			} else {

				if(mesh2 == null) {
					mesh2 = new Mesh() {
						hideFlags = HideFlags.DontSave,
						name		= string.Format("Mesh 2 '{0}'", gameObject.name)
					};
				}

				return mesh2;
			}
		}
	}

	protected virtual void LateUpdate ()
	{
		RebuildIfDirty();

		Graphics.DrawMesh(
			activeMesh,
			transform.localToWorldMatrix,
			realMaterial == null ? material : realMaterial,
			gameObject.layer
		);
	}

	public void MarkForRebuild () {
		rebuild = true;
	}

	void RebuildIfDirty () {
	
		if(rebuild) {
			activeMesh = BuildMesh();
			rebuild = false;
		}
	}

	protected abstract Mesh BuildMesh ();
}
