using System;
using Core;
using Extensions;
using Externals;
using UnityEngine;

namespace Utils
{
	public static class ConnectionUtils
	{
		/// <summary>
		/// Casts a ray from current mouse position.
		/// </summary>
		/// <param name="result">Connectable component from the storage.</param>
		/// <returns>true if ray has hit Connectable GameObject that is presented in the ConnectablesStorage.</returns>
		public static bool TryGetConnectableRaycast(out Connectable result)
		{
			result = null;
			if (!Physics.Raycast(CameraUtils.CachedCamera.ScreenPointToRay(Input.mousePosition), out var hit))
				return false;
			return Singleton<ConnectablesStorage>.Instance.TryGetConnectable(hit.collider.gameObject, out result);
		}

		public static Vector3 CorrectPlaneMovement(Vector3 prevPos, Vector3 newPos, Axis planeNormal)
		{
			switch (planeNormal)
			{
				case Axis.X:
					return newPos.Mul(new Vector3(0f, 1f, 1f)) + prevPos.Mul(new Vector3(1f, 0f, 0f));
				case Axis.Y:
					return newPos.Mul(new Vector3(1f, 0f,1f)) + prevPos.Mul(new Vector3(0f, 1f, 0f));
				case Axis.Z:
					return newPos.Mul(new Vector3(1f, 1f,0f)) + prevPos.Mul(new Vector3(0f, 0f, 1f));
				default:
					throw new ArgumentOutOfRangeException(nameof(planeNormal), planeNormal, null);
			}
		}
	}
}