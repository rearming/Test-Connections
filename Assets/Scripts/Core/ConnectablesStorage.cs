using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace Core
{
	public class ConnectablesStorage : MonoBehaviour
	{
		[SerializeField]
		private Spawner spawner;
		
		public readonly Dictionary<int, Connectable> Connectables = new Dictionary<int, Connectable>();

		private void Start()
		{
			Debug.Assert(spawner != null, $"spawner must not be null in {this.DebugObjectName()}");
			
			spawner.OnPrefabSpawn += go =>
			{
				var component = go.GetComponentInChildren<Connectable>();
				Connectables.Add(component.GetId(), component);
			};
		}

		public bool TryGetConnectable(GameObject go, out Connectable resultObject)
		{
			return Connectables.TryGetValue(go.GetInstanceID(), out resultObject);
		}
	}
}