// Copyright (C) 2006 by:
//
// Author:
//   Piotr Zurek, p.zurek@gmail.com
//
//   www.psycho-project.org
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
//

using System;
using Gtk;
using Gdk;
using Psycho;

namespace Psycho
{
        public class MainWindow : Gtk.Window
        {
                public MainWindow () : base ("Psycho")
                {
                        Icon = IconLoader.psychoIcon;

                        VBox globalVBox = new VBox ();
                        VBox mainVBox = new VBox ();
                        HPaned mainHPaned = new HPaned ();

                        MindModel model = new MindModel ();
                        model.AppendSomeNodes (model.CentralTopic);


                        MindView mapView = new MindView ();
                        MindControl mapControl = new MindControl (model, mapView);
                        mapView.WireUp (mapControl, model);

                        //OutlineView nodeView = new OutlineView ();
                        //MindControl outlineControl = new MindControl (model, nodeView);
                        //nodeView.WireUp (outlineControl, model);

                        XMLView XMLPreview = new XMLView ();
                        MindControl XMLControl = new MindControl (model, XMLPreview);
                        XMLPreview.WireUp (XMLControl, model);

                        PropertyView propertyView = new PropertyView ();
                        MindControl propertyControl = new MindControl (model, propertyView);
                        propertyView.WireUp (propertyControl, model);

                        UIManagerView UIView = new UIManagerView ();
                        MindControl uiManControl = new MindControl (model, UIView);
                        UIView.WireUp (uiManControl, model);

                        StatusView statusView = new StatusView ();
                        MindControl statusControl = new MindControl (model, statusView);
                        statusView.WireUp (statusControl, model);

                        NotesView notesView = new NotesView ();
                        MindControl notesControl = new MindControl (model, notesView);
                        notesView.WireUp (notesControl, model);

                        AddAccelGroup(UIView.uiManager.AccelGroup);

                        Expander propertyExpander = new Expander ("Property View");
                        propertyExpander.Add (propertyView);

                        Expander notesExpander = new Expander ("Notes");
                        notesView.BorderWidth = 6;
                        notesExpander.Add (notesView);
                        notesExpander.Expanded = true;

                        Notebook mainNotebook = new Notebook ();
                        mainNotebook.BorderWidth = 6;

                        mainNotebook.AppendPage (mapView, new Label ("Map View"));
                        mainNotebook.AppendPage (XMLPreview, new Label ("XML View"));
                        //mainNotebook.AppendPage (nodeView, new Label ("Outline View"));
                        Console.WriteLine ("Current page: " + mainNotebook.CurrentPage.ToString());

                        mainNotebook.ShowBorder = true;

                        globalVBox.Homogeneous = false;
                        globalVBox.PackStart (UIView.uiManager.GetWidget ("/MenuBar"), false, false, 0);
                        globalVBox.PackStart (UIView.uiManager.GetWidget ("/ToolBar"), false, false, 0);

                        mainVBox.PackStart (mainNotebook, true, true, 0);
                        Frame mainFrame = new Frame ();
                        mainFrame.Add (mainVBox);
                        mainFrame.Shadow = ShadowType.EtchedIn;
                        Frame notesFrame = new Frame ();
                        notesFrame.Add (notesExpander);
                        notesFrame.Shadow = ShadowType.EtchedIn;

                        mainHPaned.Add1 (mainFrame);
                        //mainHPaned.Add2 (notesFrame); //TODO: No notes for now.
                        //int windowWidth = new int ();
                        //int windowHeight = new int ();
                        //this.SetSizeRequest (640, 480);
                        //this.GetSize (out windowWidth, out windowHeight);
                        //mainHPaned.Position = (int) (windowWidth * 0.6);

                        globalVBox.PackStart (mainHPaned, true, true, 0);
                        globalVBox.PackStart (propertyExpander, false, false, 6);
                        globalVBox.PackEnd (statusView, false, false, 0);
                        globalVBox.ShowAll ();

                        Add (globalVBox);
                }
        }
}