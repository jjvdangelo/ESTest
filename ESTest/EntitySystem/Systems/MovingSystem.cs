namespace ESTest.EntitySystem.Systems
{
	using ESTest.EntitySystem.Components;

	public sealed class MovingSystem : SystemBase
	{
		public override void Frame(double dt)
		{
			var entities = EntityManager.GetEntitiesWithComponent<Moveable>();
			foreach(var entity in entities)
			{
				var position = entity.GetComponent<Positionable>();
				var velocity = entity.GetComponent<Moveable>();

				if (position == null || velocity == null) continue;

				position.X += velocity.DX * (float)dt;
				position.Y += velocity.DY * (float)dt;
			}
		}

		public MovingSystem(EntityManager manager)
			: base(manager) { }
	}
}