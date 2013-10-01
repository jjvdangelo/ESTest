namespace ESTest.EntitySystem
{
	public abstract class SystemBase
	{
		private readonly EntityManager _manager;
		protected EntityManager EntityManager { get { return _manager; } }

		public virtual void Initialize() { }
		public virtual void Shutdown() { }
		public abstract void Frame(double dt);

		protected SystemBase(EntityManager manager)
		{
			_manager = manager;
		}
	}
}
