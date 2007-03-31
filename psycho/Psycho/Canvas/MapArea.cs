// Copyright 2006 by:
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
        class MapView : ScrolledWindow, IView
        {
                IModel Model;
                IControl Control;

                DrawingArea mapArea;
                Viewport mapViewPort;

                Gdk.GC gc;
                Cairo.Context mapContext;
                Pango.Layout text;

                public MapView ()
                        : base ()
                {
                        mapArea = new DrawingArea ();
                        mapViewPort = new Viewport ();
                        mapArea.ExposeEvent += OnMapExpose;
                        mapArea.Realized += OnMapRealize;
                        this.Vadjustment.ValueChanged += new EventHandler (Vadjustment_ValueChanged);
                        this.ShadowType = ShadowType.EtchedIn;
                        this.HscrollbarPolicy = PolicyType.Always;
                        this.VscrollbarPolicy = PolicyType.Always;
                        this.Vadjustment.StepIncrement = 10;
                        this.Vadjustment.PageIncrement = 200;
                        this.Hadjustment.StepIncrement = 10;
                        this.Hadjustment.PageIncrement = 50;
                        this.mapViewPort.Add (mapArea);
                        this.Add (mapViewPort);
                }

                void Vadjustment_ValueChanged (object sender, EventArgs e)
                {
                        Refresh ();
                }

                void Hadjustment_ValueChanged (object sender, EventArgs e)
                {
                        Refresh ();
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
                        gc = new Gdk.GC (this.mapArea.GdkWindow);
                }

                void OnMapExpose (object sender, ExposeEventArgs args)
                {
                        mapContext = Gdk.CairoHelper.Create (args.Event.Window);
                        DrawBackground (mapContext);
                        DrawTopics (mapContext);
                        //this.Vadjustment.SetBounds (-200, Model.CentralTopic.TotalHeight + 10, 10, 10, mapArea.Allocation.Height);
                        this.mapArea.SetSizeRequest (2000, (int) Model.CentralTopic.TotalHeight);
                        ((IDisposable) mapContext.Target).Dispose ();
                        ((IDisposable) mapContext).Dispose ();
                }

                private void DrawBackground (Context iContext)
                {
                        iContext.Save ();
                        Surface background = new ImageSurface (IconLoader.paperPath);
                        SurfacePattern pattern = new SurfacePattern (background);
                        pattern.Extend = Extend.Repeat;
                        iContext.Pattern = pattern;
                        iContext.Paint ();
                        iContext.Restore ();
                        pattern.Destroy ();
                }

                void DrawTopics (Context iContext)
                {
                        DrawConnections (iContext, Model.CentralTopic);
                        DrawFrames (iContext, Model.CentralTopic);
                        DrawFrame (iContext, Model.CentralTopic);
                        DrawText (/*iContext,*/ Model.CentralTopic);
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
                        iContext.Save ();
                        iContext.Color = iTopic.Style.StrokeColor.ToCairoColor ();
                        iContext.LineWidth = iTopic.Style.StrokeWidth;

                        int reminder = (int) System.Math.IEEERemainder (iContext.LineWidth, 2);
                        if (reminder != 0) {
                                iContext.Save ();
                                iContext.Translate (0.5, 0.5);
                        }

                        iTopic.Connection.Sketch (iContext);
                        iContext.Stroke ();

                        if (reminder != 0) {
                                iContext.Restore ();
                        }

                        iContext.Restore ();
                }

                static void DrawFrame (Cairo.Context iContext, Topic iTopic)
                {
                        iContext.Save ();
                        Cairo.Color strokeColor = iTopic.Style.StrokeColor.ToCairoColor ();
                        if (iTopic.IsCurrent)
                                strokeColor = new Cairo.Color (0.75, 0.75, 0.75);
                        Cairo.Color fillColor = strokeColor;
                        iTopic.Frame.Sketch (iContext);
                        fillColor.A = 0.16;
                        iContext.Color = fillColor;
                        iContext.FillPreserve ();
                        iContext.Color = strokeColor;
                        iContext.LineWidth = iTopic.Style.StrokeWidth;

                        int reminder = (int) System.Math.IEEERemainder (iContext.LineWidth, 2);
                        if (reminder != 0) {
                                //iContext.Save ();
                                iContext.Translate (0.5, 0.5);
                        }

                        iContext.Stroke ();

                        if (reminder != 0) {
                                iContext.Translate (-0.5, -0.5);
                                //iContext.Restore ();
                        }                       

                        iContext.Restore ();
                }

                public void Update (IModel iModel)
                {
                        this.QueueDraw ();
                }

                public void Refresh ()
                {
                        this.mapArea.QueueDrawArea (0, 0 /*((int) Vadjustment.Value)*/, mapArea.Allocation.Width, mapArea.Allocation.Height);
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