using UnityEngine;
using UnityEngine.UIElements;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Controls Spec-1", menuName = "Custom Settings/Controls Spec", order = 0)]
	public class ControlsSpec : ScriptableObject
	{
		[SerializeField]
		private MouseButton moveMouseButton = MouseButton.MiddleMouse;
		public MouseButton MoveMouseButton => moveMouseButton;

		[SerializeField]
		private MouseButton connectMouseButton = MouseButton.LeftMouse;
		public MouseButton ConnectMouseButton => connectMouseButton;
	}
}