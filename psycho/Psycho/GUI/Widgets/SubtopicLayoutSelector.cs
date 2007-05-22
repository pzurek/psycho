// Copyright (C) 2007 by:
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
using System.Collections.Generic;
using Gtk;

namespace Psycho
{
        class SubtopicLayoutSelector : HBox
        {
                IView view;
                Label label;
                ComboBox combo;

                public SubtopicLayoutSelector (IView iView)
                {
                        view = iView;
                        label = new Label ("Subtopic layout: ");
                        combo = ComboBox.NewText();

                        List<string> topicLayouts = new List<string> ();

                        topicLayouts.Add ("Map");
                        topicLayouts.Add ("Tree");
                        topicLayouts.Add ("OrgChart");

                        foreach (string layout in topicLayouts)
                                combo.AppendText (layout.ToString ());

                        this.PackStart (label, false, false, 0);
                        this.PackStart (combo, false, false, 0);
                }
        }
}