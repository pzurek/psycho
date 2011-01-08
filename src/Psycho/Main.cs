//------10--------20--------30--------40--------50--------60--------70--------80
//
// Main.cs
// 
// Copyright (C) 2009 Piotr Zurek p.zurek@gmail.com
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;

using Gtk;

using Psycho.GUI;
using Psycho.Core;

namespace Psycho
{
        class MainClass
        {
                public static void Main ()
                {
                        Application.Init ();
                        MainWindow mainWindow = new MainWindow ();
                        mainWindow.DeleteEvent += OnDelete;
                        mainWindow.SetDefaultSize (640, 480);
                        mainWindow.SetPosition (WindowPosition.Center);
                        mainWindow.ShowAll();
                        Application.Run();
                }

                static void OnDelete (object sender, DeleteEventArgs args)
                {
                        Application.Quit ();
                        return;
                }
        }
}
