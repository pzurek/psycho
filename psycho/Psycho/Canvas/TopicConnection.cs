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

namespace Psycho
{
        public class TopicConnection
        {
                Topic topic;
                Cairo.Context context;
                Cairo.PointD start, end;
                Cairo.PointD middleStart, middleEnd;

                public TopicConnection (Topic iTopic)
                {
                        this.topic = iTopic;
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public Cairo.PointD Start
                {
                        get
                        {
                                start = this.topic.Frame.Left;
                                return start;
                        }
                }

                public Cairo.PointD End
                {
                        get
                        {
                                end = this.topic.Parent.Frame.Right;
                                return end;
                        }
                }

                public Cairo.PointD MiddleStart
                {
                        get
                        {
                                middleStart.X = this.topic.Parent.Frame.Right.X + ((
                                                this.topic.Frame.Left.X -
                                                this.topic.Parent.Frame.Right.X) / 2);
                                middleStart.Y = this.topic.Frame.Left.Y;
                                return middleStart;
                        }
                }

                public Cairo.PointD MiddleEnd
                {
                        get
                        {
                                middleEnd.X = this.topic.Parent.Frame.Right.X + ((
                                                this.topic.Frame.Left.X -
                                                this.topic.Parent.Frame.Right.X) / 2);
                                middleEnd.Y = this.topic.Parent.Frame.Right.Y;
                                return middleEnd;
                        }
                }

                public void Sketch (Cairo.Context iContext)
                {
                        if (this.topic.Parent == null)
                                return;
                        context = iContext;
                        context.LineCap = Cairo.LineCap.Round;
                        context.LineJoin = Cairo.LineJoin.Round;
                        switch (this.topic.Parent.Style.ConnectShape) {
                        case ConnectionShape.Straight:
                        sketchStraight ();
                        break;
                        case ConnectionShape.Crank:
                        sketchCrank ();
                        break;
                        case ConnectionShape.ChamferedCrank:
                        sketchChamferedCrank ();
                        break;
                        case ConnectionShape.RoundedCrank:
                        sketchRoundedCrank ();
                        break;
                        case ConnectionShape.Arc:
                        sketchArc ();
                        break;
                        case ConnectionShape.Curve:
                        sketchCurve ();
                        break;
                        case ConnectionShape.None:
                        break;
                        default:
                        sketchStraight ();
                        break;
                        }
                }

                private void sketchCurve ()
                {
                        context.MoveTo (Start);
                        Cairo.PointD childPoint = new Cairo.PointD (this.topic.Parent.Frame.Right.X + 10, this.topic.Frame.Left.Y);
                        Cairo.PointD parentPoint = new Cairo.PointD (this.topic.Frame.Left.X - 10, this.topic.Parent.Frame.Right.Y);
                        context.CurveTo (MiddleStart, MiddleEnd, End);
                }

                private void sketchArc ()
                {
                        context.MoveTo (this.topic.Frame.Left);

                }

                private void sketchRoundedCrank ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                private void sketchChamferedCrank ()
                {
                        throw new Exception ("The method or operation is not implemented.");
                }

                private void sketchCrank ()
                {
                        context.MoveTo (Start);
                        context.LineTo (MiddleStart);
                        context.LineTo (MiddleEnd);
                        context.LineTo (End);
                }

                private void sketchStraight ()
                {
                        context.MoveTo (Start);
                        context.LineTo (End);
                }
        }
}