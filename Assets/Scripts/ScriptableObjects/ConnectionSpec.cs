using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Connections Spec-1", menuName = "Custom Settings/Connections Spec", order = 0)]
	public class ConnectionSpec : ScriptableObject
	{
		[SerializeField]
		private Color targetSelectionsColor = Color.yellow;
		public Color TargetSelectionsColor => targetSelectionsColor;
		
		[SerializeField]
		private Color otherSelectionColor = Color.blue;
		public Color OtherSelectionColor => otherSelectionColor;
	}
}