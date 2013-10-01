namespace ESTest.EntitySystem.Systems
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	using ESTest.EntitySystem.Components;

	public sealed class DrawingSystem : SystemBase
	{
		private static readonly Brush BlackBrush = new SolidBrush(Color.Black);
		private static readonly Brush RedBrush = new SolidBrush(Color.DarkRed);
		private static readonly Brush GreenBrush = new SolidBrush(Color.DarkGreen);
		private static readonly Brush BlueBrush = new SolidBrush(Color.DarkBlue);

		private const int BufferCount = 2;

		private readonly Form _form;
		private BufferedGraphicsContext _context;
		private BufferedGraphics _buffer;


		public override void Frame(double dt)
		{
			if (_context == null || _buffer == null) return;

			{
				int count = 0;
				_buffer.Graphics.FillRectangle(BlackBrush, 0, 0, _form.Width, _form.Height);

				foreach (var entity in EntityManager.GetEntitiesWithComponent<Drawable>())
				{
					var brush = count % 3 == 0 ? RedBrush : count % 2 == 0 ? GreenBrush : BlueBrush;
					var drawable = entity.GetComponent<Drawable>();
					var position = entity.GetComponent<Positionable>();
					if (drawable == null || position == null) continue;

					_buffer.Graphics.FillRectangle(brush, position.X, position.Y, 3, 3);
					++count;
				}

				_form.Invoke(new Action(() => { _buffer.Render(); _form.Text = count + " objects rendered"; }));
			}
		}

		public override void Shutdown()
		{
			using (BlackBrush)
			using (RedBrush)
			using (GreenBrush)
			using (BlueBrush)
			{ }
		}

		public DrawingSystem(Form form, EntityManager manager)
			: base(manager)
		{
			_form = form;
			_form.Invoke(new Action(() =>
			{
				_context = BufferedGraphicsManager.Current;
				_buffer = _context.Allocate(_form.CreateGraphics(), _form.DisplayRectangle);
			}));
		}
	}
}