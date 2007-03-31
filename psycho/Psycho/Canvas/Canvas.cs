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
                Cairo.Context mapContext;
                Pango.Layout text;

                int I = 0;
                int N = 0;

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
                }

                void OnMapExpose (object o, ExposeEventArgs e)
                {
                        if ((mapArea.WidgetFlags & WidgetFlags.Realized) == 0)
                                return;

                        mapContext = Gdk.CairoHelper.Create (mapArea.GdkWindow);
                        mapContext.Antialias = Antialias.Default;
                        int w, h;
                        mapArea.GdkWindow.GetSize (out w, out h);
                        I = 0; N = 0; // TODO: Resetting coordiante values for speed testing
                        DrawBackground (mapContext);
                        //cr.Translate (w / 2, h / 2);
                        DrawTopics (mapContext);
                        ((IDisposable) mapContext.Target).Dispose ();
                        ((IDisposable) mapContext).Dispose ();
                }

                private void DrawBackground (Context iContext)
                {
                        Surface background = new ImageSurface ("Resources/paper.png");
                        SurfacePattern pattern = new SurfacePattern (background);
                        pattern.Extend = Extend.Repeat;
                        iContext.Pattern = pattern;
                        iContext.Paint ();
                        pattern.Destroy ();
                }

                void DrawTopics (Context iContext)
                {
                        //DrawRectangles (iContext, Model.CentralTopic);
                        DrawConnections (iContext, Model.CentralTopic);
                        DrawFrames (iContext, Model.CentralTopic);
                        //DrawTexts (iContext, Model.CentralTopic);
                        DrawFrame (iContext, Model.CentralTopic);
                        DrawText (/*iContext,*/ Model.CentralTopic);
                }

                public void DrawRectangles (Cairo.Context iContext, Topic iTopic)
                {
                        foreach (Topic TempTopic in iTopic.Subtopics) {
                                DrawRectangles (iContext, TempTopic);
                                DrawRectangle (iContext, iTopic);
                        }
                }

                public void DrawConnections (Cairo.Context iContext, Topic iTopic)
                {
                        foreach (Topic TempTopic in iTopic.Subtopics) {
                                if (TempTopic.IsExpanded)
                                        DrawConnections (iContext, TempTopic);
                                DrawConnection (iContext, TempTopic);
                        }
                }

                public void DrawFrames (Cairo.Context iContext, Topic iTopic)
                {
                        foreach (Topic TempTopic in iTopic.Subtopics) {
                                if (TempTopic.IsExpanded)
                                        DrawFrames (iContext, TempTopic);
                                DrawFrame (iContext, TempTopic);
                                DrawText (/*iContext, */TempTopic);
                        }
                }

                public void DrawTexts (Cairo.Context iContext, Topic iTopic)
                {
                        foreach (Topic TempTopic in iTopic.Subtopics) {
                                if (TempTopic.IsExpanded)
                                        DrawTexts (iContext, TempTopic);
                                DrawText (/*iContext, */TempTopic);
                        }
                }

                void DrawRectangle (Cairo.Context iContext, Topic iTopic)
                {
                        I = I + 20;
                        if (I / 1380 > 0) {
                                I = 0;
                                N = N + 12;
                        }
                        Cairo.Color strokeColor = iTopic.Style.StrokeColor.ToCairoColor ();
                        iContext.Rectangle (I, N, 16, 10);
                        Cairo.Color fillColor = strokeColor;
                        fillColor.A = 0.16;
                        iContext.Color = fillColor;
                        iContext.FillPreserve ();
                        iContext.Color = strokeColor;
                        iContext.Stroke ();
                }

                void DrawText (/*Cairo.Context iContext,*/ Topic iTopic)
                {
                        gc = mapArea.Style.TextAAGC (StateType.Normal);
                        gc.Foreground = new Gdk.Color (0, 0, 0);
                        text = iTopic.TextLayout;
                        mapArea.GdkWindow.DrawLayout (gc, (int) iTopic.Offset.X, (int) iTopic.Offset.Y, text);
                        gc.Dispose ();
                }

                static void DrawConnection (Cairo.Context iContext, Topic iTopic)
                {
                        iTopic.Connection.Sketch (iContext);
                        iContext.Color = iTopic.Style.StrokeColor.ToCairoColor ();
                        iContext.LineWidth = iTopic.Style.StrokeWidth;
                        iContext.Stroke ();
                }

                static void DrawFrame (Cairo.Context iContext, Topic iTopic)
                {
                        Cairo.Color strokeColor = iTopic.Style.StrokeColor.ToCairoColor ();
                        if (iTopic.IsCurrent)
                                strokeColor = new Cairo.Color (0.75, 0.75, 0.75);
                        Cairo.Color fillColor = strokeColor;
                        iTopic.Frame.Sketch (iContext);
                        fillColor.A = 0.16;
                        iContext.Color = fillColor;
                        iContext.FillPreserve ();
                        iContext.Color = strokeColor;
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