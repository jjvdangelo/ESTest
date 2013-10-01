namespace ESTest.EntitySystem.Components
{
	public sealed class Positionable
	{
		public float X, Y, Z;

		public Positionable(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Positionable() { }
	}
}