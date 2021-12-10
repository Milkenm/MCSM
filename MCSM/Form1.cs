using MCSM_Core;

using System;
using System.Windows.Forms;

namespace MCSM
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			this.InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			ServerPing.Ping("mc.hypixel.net");
		}
	}
}
