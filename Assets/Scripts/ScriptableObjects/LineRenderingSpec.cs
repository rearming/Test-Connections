using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Line Rendering Spec-1", menuName = "Custom Settings/Line Rendering Spec", order = 0)]
	public class LineRenderingSpec : ScriptableObject
	{
		[SerializeField]
		private float width = 0.4f;
		public float Width => width;

		[SerializeField]
		[Tooltip("Line gradient will be chosen randomly from this list.")]
		private List<Gradient> gradients = new List<Gradient> { new Gradient() };
		
		public Gradient Gradient => gradients.RandomElement();
	}
}