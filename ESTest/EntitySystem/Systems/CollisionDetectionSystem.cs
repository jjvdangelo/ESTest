namespace ESTest.EntitySystem.Systems
{
	using System.Windows.Forms;
	using ESTest.EntitySystem.Components;

	public sealed class CollisionDetectionSystem : SystemBase
	{
		private float width, height;

		public override void Frame(double dt)
		{
			var entities = EntityManager.GetEntitiesWithComponent<Moveable>();
			foreach (var entity in entities)
			{
				var position = entity.GetComponent<Positionable>();
				var velocity = entity.GetComponent<Moveable>();

				if (position.X <= 0)
				{
					position.X = 0;
					if (velocity.DX < 0) velocity.DX *= -1;
				} else if (position.X - 5 > width)
				{
					position.X = width - 15;
					if  (velocity.DX > 0) velocity.DX *= -1;
				}

				if (position.Y <= 0)
				{
					position.Y = 0;
					if(velocity.DY < 0) velocity.DY *= -1;
				} else if (position.Y - 5 > height)
				{
					position.Y = height;
					if (velocity.DY > 0) velocity.DY *= -1;
				}
			}
		}

		public CollisionDetectionSystem(Form form, EntityManager manager)
			: base(manager)
		{
			width = form.Width;
			height = form.Height;
		}
	}
}