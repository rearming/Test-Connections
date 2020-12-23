namespace Core.Interfaces
{
	public interface IMover
	{
		void Move(IMovable movable);
		void Reset();
	}
}