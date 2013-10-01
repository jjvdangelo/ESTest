namespace ESTest.EntitySystem.Components
{
	public sealed class Moveable
	{
		public float DX, DY;

		public Moveable(float dx, float dy)
		{
			DX = dx;
			DY = dy;
		}

		public Moveable() { }
	}
}