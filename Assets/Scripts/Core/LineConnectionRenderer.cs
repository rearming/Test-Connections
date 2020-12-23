using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Extensions;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
	public class LineConnectionRenderer : MonoBehaviour, IConnectionRenderer
	{
		[SerializeField]
		private LineRenderingSpec renderingSpec;
		
		private LineRenderer _lineRenderer;
		private List<Transform> _targets;

		public void Setup(List<Transform> targets)
		{
			_targets = targets;
		}

		public void Break()
		{
			_targets.Clear();
		}

		private void Awake()
		{
			_lineRenderer = GetComponent<LineRenderer>();

			Debug.Assert(_lineRenderer != null, $"_lineRenderer must not be null in {this.DebugObjectName()}!");
		}

		private void Start()
		{
			_lineRenderer.widthMultiplier = renderingSpec.Width;
			_lineRenderer.colorGradient = renderingSpec.Gradient;
		}

		private void Update()
		{
			_lineRenderer.positionCount = _targets.Count;
			_lineRenderer.SetPositions(_targets.Select(t => t.position).ToArray());
		}
	}
}