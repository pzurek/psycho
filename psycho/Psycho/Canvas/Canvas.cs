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
using System.Collections.Generic;
using System.Text;
using Gtk;
using Cairo;

namespace Psycho
{
        class Canvas : DrawingArea, IView
        {
                IModel Model;
                IControl Control;

                public Canvas ()
                {
                }

                public void WireUp (IControl iControl, IModel iModel)
                {
                        if (Model != null) {
                                Model.RemoveObserver (this);
                        }

                        Model = iModel;
                        Control = iControl;

                        Control.SetModel (Model);
                        Control.SetView (this);
                        Model.AddObserver (this);
                        Update (Model);
                }

                protected override bool OnExposeEvent (Gdk.EventExpose e)
                {
                        Context cr = Gdk.CairoHelper.Create (e.Window);
                        int w, h;
                        e.Window.GetSize (out w, out h);
                        DrawBackground (cr);
                        DrawTopics (cr, w, h);
                        return true;
                }

                private void DrawBackground (Context cr)
                {
                        Surface background = new ImageSurface ("Resources/paper.png");
                        SurfacePattern pattern = new SurfacePattern (background);
                        pattern.Extend = Extend.Repeat;
                        cr.Pattern = pattern;
                        cr.Paint ();
                }

                Pango.Layout text;
                Cairo.Matrix matrix;

                void DrawTopics (Cairo.Context cr, int w, int h)
                {
                        this.QueueDraw ();
                        Model.CentralTopic.ForEach (delegate (Topic topic)
                        {
                                DrawFrame (cr, topic);
                                DrawText (cr, topic);
                        }
                        );
                }

                void DrawText (Cairo.Context iContext, Topic iTopic)  //TODO: Implement draing to cairo context
                {
                        text = iTopic.TextLayout;
                        this.GdkWindow.DrawLayout
                                (this.Style.TextAAGC (StateType.Normal),
                                iTopic.Offset.X,
                                iTopic.Offset.Y,
                                text);
                }

                void DrawFrame (Cairo.Context iContext, Topic iTopic)
                {
                        iTopic.Frame.Draw (iContext, iTopic);
                        Console.WriteLine ("Frame drawn for: " + iTopic.Text);
                }

                public void Update (IModel iModel)
                {
                }

                public void AddTopic ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void AddSubtopic ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void DeleteTopic ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void CommitChange (Topic iTopic)
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void ExpandTopic (string iGuid, bool isExpanded)
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void EditTitle (string Title)
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void SetCurrentTopic ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void TriggerEdit (bool editPending)
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void DisableAddSibling ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void DisableDelete ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void EnableAddSibling ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                public void EnableDelete ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }
        }
}