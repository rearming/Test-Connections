using System;
using Core.Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
	public class Connectable : MonoBehaviour, IMovable
	{
		[SerializeField]
		private ConnectionSpec connectionSpec;
		
		private Material _targetMaterial;
		private Color _targetBaseColor;

		private void Start()
		{
			_targetMaterial = GetComponent<Renderer>().material;
			_targetBaseColor = _targetMaterial.color;
		}

		public void Move(Vector3 newPos)
		{
			transform.position = newPos;
		}

		public Vector3 Position => transform.position;

		public void Recolor(ConnectionState connectionState)
		{
			switch (connectionState)
			{
				case ConnectionState.SelectedTarget:
					_targetMaterial.color = connectionSpec.TargetSelectionsColor;
					break;
				case ConnectionState.SelectedOther:
					_targetMaterial.color = connectionSpec.OtherSelectionColor;
					break;
				case ConnectionState.Deselected:
					_targetMaterial.color = _targetBaseColor;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(connectionState), connectionState, null);
			}
		}

		public int GetId()
		{
			return gameObject.GetInstanceID();
		}
	}
}