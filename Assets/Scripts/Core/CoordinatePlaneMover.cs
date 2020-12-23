using System;
using Core.Interfaces;
using Extensions;
using UnityEngine;
using Utils;

namespace Core
{
	public class CoordinatePlaneMover : MonoBehaviour, IMover
	{
		[SerializeField]
		private Coordinate lockedAxis = Coordinate.Y;

		[Header("Gizmos Drawing")]
		
		[SerializeField]
		private bool drawGizmos = true;

		[SerializeField]
		private float gizmosPlaneSize = 4f;

		[SerializeField]
		[Range(0f, 1f)]
		private float gizmosPlaneAlpha = 0.2f;
		
		private IMovable _movable;

		public void Move(IMovable movable)
		{
			_movable = movable;
			
			var plane = GetMovementPlane(_movable.Position);
			var ray = CameraUtils.CachedCamera.ScreenPointToRay(Input.mousePosition);
			if (!plane.Raycast(ray, out var distance))
				return;
			var newPos = ray.GetPoint(distance);
			var correctedPos = ConnectionUtils.CorrectPlaneMovement(_movable.Position, newPos, lockedAxis);
			_movable.Move(correctedPos);
		}

		public void Reset()
		{
			_movable = null;
		}

		private void Update()
		{
			SetMoveCoordinate();
		}

		private void SetMoveCoordinate()
		{
			if (Input.GetKeyDown(KeyCode.X))
				lockedAxis = Coordinate.X;
			else if (Input.GetKeyDown(KeyCode.Y))
				lockedAxis = Coordinate.Y;
			else if (Input.GetKeyDown(KeyCode.Z))
				lockedAxis = Coordinate.Z;
		}

		private Plane GetMovementPlane(Vector3 targetPos)
		{
			Vector3 normal;
			
			switch (lockedAxis)
			{
				case Coordinate.X:
					normal = Vector3.right;
					break;
				case Coordinate.Y:
					normal = Vector3.up;
					break;
				case Coordinate.Z:
					normal = Vector3.forward;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return new Plane(normal, targetPos);
		}
		
		private void OnDrawGizmos()
		{
			if (!drawGizmos) 
				return;
			if (_movable == null)
				return;
			
			GizmosDrawPlane(
				_movable.Position, 
				GetMovementPlane(_movable.Position),
				GetGizmosPlaneColor(lockedAxis).SetA(gizmosPlaneAlpha),
				gizmosPlaneSize);
		}

		private void GizmosDrawPlane(Vector3 center, Plane plane, Color color, float size)
		{
			var rotation = Quaternion.LookRotation(transform.TransformDirection(plane.normal));
			var trs = Matrix4x4.TRS(transform.TransformPoint(center), rotation, Vector3.one);
			Gizmos.matrix = trs;
			Gizmos.color = color;
			
			Gizmos.DrawCube(Vector3.zero, new Vector3(size, size, 0.0001f));
			
			Gizmos.matrix = Matrix4x4.identity;
			Gizmos.color = Color.white;
		}

		private Color GetGizmosPlaneColor(Coordinate normal)
		{
			switch (normal)
			{
				case Coordinate.X:
					return Color.red;
				case Coordinate.Y:
					return Color.green;
				case Coordinate.Z:
					return Color.blue;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}