namespace ESTest
{
	using System;
	using System.Diagnostics;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows.Forms;
	using ESTest.EntitySystem;
	using ESTest.EntitySystem.Components;
	using ESTest.EntitySystem.Systems;

	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (var source = new CancellationTokenSource())
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				var manager = new EntityManager();
				var form = new Form1(manager);
				var systems = new SystemBase[] {
					new MovingSystem(manager),
					new CollisionDetectionSystem(form, manager)
				};

				BuildEntities(manager);
				form.FormClosing += (o, e) => source.Cancel();
				form.Load += (o, e) => Run(new DrawingSystem(form, manager), systems, source.Token);

				Application.Run(form);
			}
		}

		static void Run(DrawingSystem drawing, SystemBase[] systems, CancellationToken token)
		{
			Task.Factory.StartNew(() =>
			{
				foreach (var system in systems) system.Initialize();

				var watch = Stopwatch.StartNew();
				double t = 0;

				while (token.IsCancellationRequested == false)
				{
					var currentTick = watch.Elapsed.TotalSeconds;
					var frameTime = currentTick - t;
					t = currentTick;

					foreach(var system in systems)
					{
						system.Frame(frameTime);
					}

					drawing.Frame(frameTime);
				}
				watch.Stop();

				drawing.Shutdown();
				foreach (var system in systems) system.Shutdown();
			}, token);
		}

		static void BuildEntities(EntityManager manager)
		{
			var random = new Random();
			for (int i = 0; i < 5000; ++i)
			{
				float dxMul = i % random.Next(1, 4) == 0 ? -1 : 1;
				float dyMul = i % random.Next(1, 4) == 0 ? -1 : 1;

				dxMul *= i % random.Next(1, 25) == 0 ? 1.25f : 1.0f;
				dyMul *= i % random.Next(1, 25) == 0 ? -1.15f : 1.05f;

				var x = random.Next(0, 800);
				var y = random.Next(0, 600);

				var square = manager.CreateEntity();
				square.AddComponent(new Positionable(x, y, 0));
				square.AddComponent(new Drawable());
				square.AddComponent(new Moveable(50 * dxMul, 50 * dyMul));
			}
		}
	}
}