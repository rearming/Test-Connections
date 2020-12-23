using System.Linq;
using Extensions;
using Externals;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Core
{
	public abstract class Connector : MonoBehaviour
	{
		[SerializeField]
		protected ControlsSpec controlsSpec;

		[SerializeField]
		protected GameObject connectionRendererPrefab;

		protected (Connectable, Connectable) Connectables = (null, null);

		protected void Deselect()
		{
			Singleton<ConnectablesStorage>.Instance.Connectables.Values.
				ForEach(c => c.Recolor(ConnectionState.Deselected));
			Connectables.Item1 = null;
			Connectables.Item2 = null;
		}
		
		private void OnEnable()
		{
			if (!Singleton<SceneInfo>.Instance.IsSceneLoaded) // prevent initial switching on scene/object first load
				return;
			FindObjectsOfType<Connector>()
				.Where(c => c.GetInstanceID() != GetInstanceID())
				.ForEach(c => c.gameObject.SetActive(false));
		}
	}
}