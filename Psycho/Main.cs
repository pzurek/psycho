// project created on 20/07/2006 at 8:23 p
using System;
using Gtk;

namespace Psycho
{
	class MainClass
	{
		public static void Main()
		{
			Console.WriteLine("Hello World!");
			Application.Init ();

        	MainWindow mainWindow = new MainWindow();
            mainWindow.DeleteEvent += OnDelete;
            mainWindow.SetDefaultSize (640, 480);
            mainWindow.SetPosition(WindowPosition.Center);
            
            MindModel model = new MindModel();
            Console.WriteLine("Creating model");
            TemporaryButtonBox view = new TemporaryButtonBox();
            Console.WriteLine("Creating view");
            MindControl control = new MindControl(model, view);
            Console.WriteLine("Creating controller");
            view.WireUp(control, model);
            Console.WriteLine("The view connected to the model and controller");

			mainWindow.Add(view);
            mainWindow.ShowAll ();
            Console.WriteLine("Small break");
	        Application.Run ();
		}
		
		static void OnDelete (object o, DeleteEventArgs e)
	    {
	        Application.Quit ();
	    }     
	}
}
