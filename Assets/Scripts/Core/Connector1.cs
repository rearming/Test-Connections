using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Extensions;
using Externals;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Core
{
	public sealed class Connector1 : Connector
	{
		private void Update()
		{
			if (Input.GetMouseButtonDown((int) controlsSpec.ConnectMouseButton))
				Select();
		}

		private void Select()
		{
			if (!ConnectionUtils.TryGetConnectableRaycast(out var connectable) ||
				!Connectables.Item1.IsRealNull() && Connectables.Item1.GetId() == connectable.GetId())
			{
				Deselect();
				return;
			}

			if (Connectables.Item1.IsRealNull()) // if first connection
			{
				Connectables.Item1 = connectable;
				Singleton<ConnectablesStorage>.Instance.Connectables.Values.
					ForEach(c => c.Recolor(ConnectionState.SelectedOther));
				Connectables.Item1.Recolor(ConnectionState.SelectedTarget);
				return;
			}
			
			Connect(connectable);
		}

		private void Connect(Connectable secondConnectable)
		{
			Connectables.Item2 = secondConnectable;

			var connectionRendererObj = Instantiate(connectionRendererPrefab);
			var connectionRenderer = connectionRendererObj.GetComponent<IConnectionRenderer>();
			connectionRenderer.Setup(new List<Transform> { Connectables.Item1.transform, Connectables.Item2.transform });
			
			Deselect();
		}
	}
}