using UnityEngine;

namespace Extensions
{
	public static class UnityExtensions
	{
		public static string DebugObjectName(this MonoBehaviour monoBehaviour)
		{
			return $"[{monoBehaviour.gameObject.name} - {monoBehaviour.name}]";
		}

		public static bool IsRealNull(this Object unityObject)
		{
			// ReSharper disable once RedundantCast.0
			return (object) unityObject == null;
		}

		/// <summary>
		/// Multiplies two vectors component-wise.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Vector3 Mul(this Vector3 a, Vector3 b) => new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		
		public static Color SetA(this Color color, float a)
		{
			var col = color;
			col.a = a;
			return col;
		}
	}
}