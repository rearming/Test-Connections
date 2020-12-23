using System.Collections.Generic;
using UnityEngine;

namespace Core.Interfaces
{
	public interface IConnectionRenderer
	{
		void Setup(List<Transform> targets);
		void Break();
	}
}