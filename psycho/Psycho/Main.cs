// Copyright (C) 2006 by:
//
// Author:
//   Piotr Zurek
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
            Console.WriteLine("Creating model", 3);
            model.AppendSomeNodes(model.CentralTopic);
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
