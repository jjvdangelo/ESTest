namespace ESTest
{
	using System;
	using System.Windows.Forms;
	using ESTest.EntitySystem;

	public partial class Form1 : Form
	{
		private EntityManager _manager;

		public Form1(EntityManager manager)
		{
			InitializeComponent();
			_manager = manager;
		}

		public void Form_Close(object sender, EventArgs e)
		{
			MessageBox.Show("Closing");
		}
	}
}