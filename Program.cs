using System;
using System.IO;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;

namespace WiimoteTest
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
            AsynchronousClient.StartClient();
            //FileStream filestream = new FileStream("out.txt", FileMode.Create);
            //streamwriter = new StreamWriter(filestream);
            //streamwriter.AutoFlush = true;
            //Console.SetOut(streamwriter);
            //Console.SetError(streamwriter);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MultipleWiimoteForm());
		}

        public static StreamWriter streamwriter { get; set; }
    }
}