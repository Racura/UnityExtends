using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	[SerializeField] RangeInt rangeInt	= new RangeInt(0, 1);
	[SerializeField] RangeF rangeFloat	= new RangeF(0, 1);
	[SerializeField] InsetF insetF;
	[SerializeField][Selection(0,1,2,4,8,16)] int selectionSample;
}
