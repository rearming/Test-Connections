using Core.Interfaces;
using Extensions;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Core
{
	public class MovementInputHandler : MonoBehaviour
	{
		[SerializeField]
		private ControlsSpec controlsSpec;
		
		private MouseDragInputHandler _mouseDragInputHandler;
		private IMovable _movable;
		private IMover _mover;

		private void Awake()
		{
			_mover = GetComponent<IMover>();
			_mouseDragInputHandler = GetComponent<MouseDragInputHandler>();
			
			Debug.Assert(_mover != null, $"_mover must not be null in {this.DebugObjectName()}!");
			Debug.Assert(_mouseDragInputHandler != null, $"_mouseDragInputHandler must not be null in {this.DebugObjectName()}!");
		}

		private void Start()
		{
			_mouseDragInputHandler.Setup(controlsSpec.MoveMouseButton, OnDragBegin, OnDrag, OnDragEnd);
		}

		private bool OnDragBegin()
		{
			if (!ConnectionUtils.TryGetConnectableRaycast(out var connectable))
				return false;
			_movable = connectable.gameObject.GetComponent<IMovable>();
			return true;
		}

		private void OnDrag()
		{
			_mover.Move(_movable);
		}

		private void OnDragEnd()
		{
			_mover.Reset();
		}
	}
}