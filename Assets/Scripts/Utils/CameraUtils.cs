using UnityEngine;

namespace Utils
{
	public static class CameraUtils
	{
		private static Camera cachedCamera;
		public static Camera CachedCamera
		{
			get
			{
				if (cachedCamera == null)
					cachedCamera = Camera.main;
				return cachedCamera;
			}
		}
	}
}