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
//

using System;
using Gtk;
using Psycho;

namespace Psycho {
    public class MainWindow : Window {
        public MainWindow ()
            : base("Psycho")
        {
            IconLoader iconLoader = new IconLoader();
            Icon = iconLoader.topicIcon;

            VBox mainVBox = new VBox();

            MindModel model = new MindModel();
            model.AppendSomeNodes(model.CentralTopic);

            ButtonView buttonView = new ButtonView();
            MindControl buttonControl = new MindControl(model, buttonView);
            buttonView.WireUp(buttonControl, model);

            OutlineView nodeView = new OutlineView();
            MindControl outlineControl = new MindControl(model, nodeView);
            nodeView.WireUp(outlineControl, model);

            UIManagerView UIView = new UIManagerView();
            MindControl uiManControl = new MindControl(model, UIView);
            UIView.WireUp(uiManControl, model);

            StatusView statusView = new StatusView();
            MindControl statusControl = new MindControl(model, statusView);
            statusView.WireUp(statusControl, model);

            //AddAccelGroup(UIView.uiManager.AccelGroup);

            Expander buttonExpander = new Expander("Button View");
            buttonExpander.Add(buttonView);

            Notebook mainNotebook = new Notebook();
            mainNotebook.BorderWidth = 6;

            DrawingArea mapView = new DrawingArea();
            TextView XMLView = new TextView();

            mainNotebook.InsertPage(mapView, new Label("Map View"), /*new Label("Map View"),*/ 0);
            mainNotebook.InsertPage(nodeView, new Label("Outline View"), /*new Label("Outline View"),*/ 1);
            mainNotebook.InsertPage(XMLView, new Label("XML View"), /*new Label("XML View"),*/ 2);

            mainVBox.Homogeneous = false;

            mainVBox.PackStart(UIView.uiManager.GetWidget("/MenuBar"), false, false, 0);
            mainVBox.PackStart(UIView.uiManager.GetWidget("/ToolBar"), false, false, 0);

            mainVBox.PackStart(buttonExpander, false, false, 6);
            mainVBox.PackStart(mainNotebook, true, true, 6);
            mainVBox.PackEnd(statusView, false, false, 0);
            mainVBox.ShowAll();

            Add(mainVBox);
        }
    }
}
