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
using System.Text;
using Gtk;

namespace Psycho
{
        class TopicLayoutComboBox : ComboBox
        {
                IView view;
                IModel model;
                Topic topic;
                TopicStyle style;
                List<string> topicLayouts;

                public TopicLayoutComboBox (IView iView, IModel iModel) : base ()
                {
                        this.Changed += new EventHandler(TopicLayoutComboBox_Changed);
                        this.ExposeEvent += new ExposeEventHandler(TopicLayoutComboBox_ExposeEvent);
                }

                public void Update (IView iView, IModel iModel)
                {
                        view = iView;
                        model = iModel;
                        Fill ();
                }

                void CheckSelectionState()
                {
                        if (model == null || model.CurrentTopic == null)
                                Sensitive = false;
                        else {
                                Fill ();
                                Sensitive = true;
                        }
                }

                public void Fill ()
                {
                        topic = model.CurrentTopic;
                        if (topicLayouts != null)
                                topicLayouts.Clear ();
                        else
                                topicLayouts = new List<string> ();

                        if (topic.IsCentral) {
                                topicLayouts.Add ("Map");
                                topicLayouts.Add ("Map - Left");
                                topicLayouts.Add ("Map - Right");
                                topicLayouts.Add ("Tree");
                                topicLayouts.Add ("Tree - Left");
                                topicLayouts.Add ("Tree - Right");
                                topicLayouts.Add ("OrgChart - Down");
                                topicLayouts.Add ("OrgChart - Up");
                                topicLayouts.Add ("OrgChart - Double");
                        }
                        else {
                                topicLayouts.Add ("Map");
                                topicLayouts.Add ("Tree");
                                topicLayouts.Add ("OrgChart");
                        }

                        foreach (string layout in topicLayouts)
                                this.InsertText (topicLayouts.IndexOf (layout), layout.ToString ());
                }

                void TopicLayoutComboBox_Changed (object o, EventArgs args)
                {
                        view.EditStyle (style);
                }

                void TopicLayoutComboBox_ExposeEvent (object o, ExposeEventArgs args)
                {
                        CheckSelectionState ();
                }
        }
}