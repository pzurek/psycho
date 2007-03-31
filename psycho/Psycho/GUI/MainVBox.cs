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

namespace Psycho
{
	public class MainVBox : VBox
	{
		public MainVBox() : base()
		{
            MindModel model = new MindModel();
            Console.WriteLine("Creating model");
            model.AppendSomeNodes(model.CentralTopic);

            TemporaryButtonBox buttonView = new TemporaryButtonBox();
//            PsychoNodeView outlineView = new PsychoNodeView();
//            ScrolledWindow outlineContainer = new ScrolledWindow();
            PsychoOutlineView outlineView = new PsychoOutlineView();

            Console.WriteLine("Creating button view");
            MindControl buttonControl = new MindControl(model, buttonView);
            Console.WriteLine("Creating controller");
            buttonView.WireUp(buttonControl, model);
            Console.WriteLine("The button view connected to the model and controller");

            Console.WriteLine("Creating outline view");
            MindControl outlineControl = new MindControl(model, outlineView);
            Console.WriteLine("Creating controller");
//            outlineContainer.Add(outlineView);
            outlineView.WireUp(outlineControl, model);
            Console.WriteLine("The outline view connected to the model and controller");
            ShowAll();

			this.Homogeneous = false;
			this.PackStart(buttonView, false, false, 6);
            this.PackStart(outlineContainer, true, true, 6);
			this.BorderWidth = 6;
		}
	}
}
