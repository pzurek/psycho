// Copyright (C) 2006 by:
//
// Author:
//   Piotr Zurek, p.zurek@gmail.com
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
using Gdk;
using Cairo;

namespace Psycho
{
        class Canvas : ScrolledWindow, IView
        {
                IModel Model;
                IControl Control;

                DrawingArea mapArea;

                Gdk.GC gc;
                Gdk.Color red;
                Gdk.Color green;
                Gdk.Color blue;
                Gdk.Color yellow;
                Gdk.Color black;

                Pango.Layout text;

                public Canvas ()
                        : base ()
                {
                        mapArea = new DrawingArea ();

                        mapArea.ExposeEvent += OnMapExpose;
                        mapArea.Realized += OnMapRealize;
                        this.HscrollbarPolicy = PolicyType.Always;
                        this.VscrollbarPolicy = PolicyType.Always;
                        this.Add (mapArea);
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

                void OnMapRealize (object sender, EventArgs e)
                {
                        gc = new Gdk.GC (this.GdkWindow);
                        
                        red = new Gdk.Color (0xff, 0, 0);
                        green = new Gdk.Color (0, 0xff, 0);
                        blue = new Gdk.Color (0, 0, 0xff);
                        black = new Gdk.Color (0, 0, 0);

                        Colormap colormap = Colormap.System;
                        colormap.AllocColor (ref red, true, true);
                        colormap.AllocColor (ref green, true, true);
                        colormap.AllocColor (ref blue, true, true);
                        colormap.AllocColor (ref black, true, true);
                }


                void OnMapExpose (object o, ExposeEventArgs e)
                {
                        Context cr = Gdk.CairoHelper.Create (mapArea.GdkWindow);
                        int w, h;
                        mapArea.GdkWindow.GetSize (out w, out h);
                        DrawBackground (cr);
                        DrawTopics (cr);
                }

                private void DrawBackground (Context cr)
                {
                        Surface background = new ImageSurface ("Resources/paper.png");
                        SurfacePattern pattern = new SurfacePattern (background);
                        pattern.Extend = Extend.Repeat;
                        cr.Pattern = pattern;
                        cr.Paint ();
                }

                void DrawTopics (Cairo.Context cr)
                {
                        Model.CentralTopic.ForEach (delegate (Topic topic)
                        {
                                if (topic.IsVisible || topic.IsCentral) {
                                        DrawFrame (cr, topic);
                                        DrawText (cr, topic);
                                }
                        }
                        );
                }

                void DrawText (Cairo.Context iContext, Topic iTopic)
                {
                        gc.Foreground = black;
                        text = iTopic.TextLayout;
                        this.mapArea.GdkWindow.DrawLayout (gc, iTopic.Offset.X, iTopic.Offset.Y, text);
                }

                void DrawFrame (Cairo.Context iContext, Topic iTopic)
                {
                        iTopic.Frame.Sketch (iContext, iTopic);
                        Cairo.Color strokeColor;
                        Cairo.Color fillColor;
                        if (iTopic.IsCurrent)
                                strokeColor = new Cairo.Color (0.5, 0.5, 0.5);
                        else
                                strokeColor = new Cairo.Color (0, 0, 1);
                        iContext.Color = strokeColor;
                        iContext.LineWidth = iTopic.Style.StrokeWidth;
                        iContext.Stroke ();
                }

                public void Update (IModel iModel)
                {
                        this.QueueDraw ();
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