using System;
using System.Collections.Generic;
using Core.Interfaces;
using Extensions;
using Externals;
using UnityEngine;
using Utils;

namespace Core
{
	public sealed class Connector2 : Connector
	{
		private MouseDragInputHandler _mouseDragInputHandler;

		private Transform _invisibleMouseTransform;
		private IConnectionRenderer _currentConnectionRenderer;

		private void Awake()
		{
			_mouseDragInputHandler = GetComponent<MouseDragInputHandler>();
			
			Debug.Assert(_mouseDragInputHandler != null, $"_mouseDragInputHandler must not be null in {this.DebugObjectName()}!");
		}

		private void Start()
		{
			_mouseDragInputHandler.Setup(controlsSpec.ConnectMouseButton, OnDragBegin, OnDrag, OnDragEnd);
			CreateInvisibleMouseTransform();
		}

		private void CreateInvisibleMouseTransform()
		{
			var mouseTransformGO = new GameObject("InvisibleMouseTransform");
			mouseTransformGO.transform.parent = transform;
			_invisibleMouseTransform = mouseTransformGO.transform;
		}

		private void UpdateLineEnd()
		{
			var plane = new Plane(Vector3.up, Connectables.Item1.Position);
			var ray = CameraUtils.CachedCamera.ScreenPointToRay(Input.mousePosition);
			if (plane.Raycast(ray, out var distance))
				_invisibleMouseTransform.position = ray.GetPoint(distance);
		}

		private bool OnDragBegin()
		{
			if (!ConnectionUtils.TryGetConnectableRaycast(out var connectable))
				return false;
			
			Connectables.Item1 = connectable;
			Singleton<ConnectablesStorage>.Instance.Connectables.Values.
				ForEach(c => c.Recolor(ConnectionState.SelectedOther));
			Connectables.Item1.Recolor(ConnectionState.SelectedTarget);
			
			var connectionRendererObj = Instantiate(connectionRendererPrefab);
			_currentConnectionRenderer = connectionRendererObj.GetComponent<IConnectionRenderer>();
			_currentConnectionRenderer.Setup(new List<Transform>{ Connectables.Item1.transform, _invisibleMouseTransform });
			UpdateLineEnd();
			return true;
		}

		private void OnDrag()
		{
			if (!ConnectionUtils.TryGetConnectableRaycast(out var connectable))
			{
				if (Connectables.Item2.IsRealNull())
					_currentConnectionRenderer.Setup(new List<Transform>{ Connectables.Item1.transform, _invisibleMouseTransform });
				// ReSharper disable once Unity.NoNullPropagation
				Connectables.Item2?.Recolor(ConnectionState.SelectedOther);
				Connectables.Item2 = null;
				UpdateLineEnd();
				return;
			}
			if (connectable.GetId() == Connectables.Item1.GetId()) // if same connectable, do nothing
				return;
			
			Connectables.Item2 = connectable;
			Connectables.Item2.Recolor(ConnectionState.SelectedTarget);
			_currentConnectionRenderer.Setup(new List<Transform>{ Connectables.Item1.transform, Connectables.Item2.transform });
		}

		private void OnDragEnd()
		{
			if (Connectables.Item1 != null && Connectables.Item2 == null)
				_currentConnectionRenderer?.Break();
			Connectables = (null, null);
			_currentConnectionRenderer = null;
			Deselect();
		}
	}
}