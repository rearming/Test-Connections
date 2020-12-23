using System;
using UnityEngine;

namespace Utils
{
	public class SceneInfo : MonoBehaviour
	{
		public bool IsSceneLoaded { get; private set; }
		
		private void Start()
		{
			IsSceneLoaded = true;
		}
	}
}