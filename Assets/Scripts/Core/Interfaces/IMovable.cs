using UnityEngine;

namespace Core.Interfaces
{
	public interface IMovable
	{
		void Move(Vector3 newPos);
		Vector3 Position { get; }
	}
}