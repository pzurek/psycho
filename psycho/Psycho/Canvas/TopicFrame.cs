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
        public class TopicFrame
        {
                Cairo.Context context;
                double frameHeight, frameWidth;
                Cairo.PointD origin, center, left, right, top, bottom;
                double polyDist;
                double octDist, octDistHor, octDistVer;
                double hexDist, hexDistHor;
                double radius;
                Topic topic;

                static double sqrt2 = System.Math.Sqrt (2);
                static double sqrt3 = System.Math.Sqrt (3);
                static double PI = System.Math.PI;
                static double angle1 = 0.0 * (PI / 180.0);
                static double angle2 = 90.0 * (PI / 180.0);
                static double angle3 = 180.0 * (PI / 180.0);
                static double angle4 = 270.0 * (PI / 180.0);

                public TopicFrame (Topic iTopic)
                {
                        this.topic = iTopic;
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public double Width
                {
                        get
                        {
                                frameWidth = this.Topic.TextWidth +
                                             this.Topic.Style.LeftMargin +
                                             this.Topic.Style.RightMargin;
                                return frameWidth;
                        }
                }

                public double Height
                {
                        get
                        {
                                frameHeight = this.Topic.TextHeight +
                                              this.Topic.Style.TopMargin +
                                              this.Topic.Style.BottomMargin;
                                return frameHeight;
                        }
                }

                double OctDistHor
                {
                        get
                        {
                                octDistHor = new double ();
                                if (this.Topic.Style.Shape == TopicShape.Octagon)
                                        octDistHor = this.Height / (2 + sqrt2);
                                else
                                        octDistHor = 0;
                                return octDistHor;
                        }
                }

                double OctDistVer
                {
                        get
                        {
                                octDistVer = new double ();
                                if (this.Topic.Style.Shape == TopicShape.Octagon)
                                        octDistVer = (this.Height - OctDistHor) / 2;
                                else
                                        octDist = 0;
                                return octDistVer;
                        }
                }

                double OctDist
                {
                        get
                        {
                                octDist = new double ();
                                octDist = OctDistHor * sqrt2;
                                return octDist;
                        }
                }

                double HexDistHor
                {
                        get
                        {
                                hexDistHor = new double ();
                                if (this.Topic.Style.Shape == TopicShape.Hexagon) {
                                        hexDistHor = this.Height / (2 * sqrt3);
                                }
                                else
                                        hexDistHor = 0;
                                return hexDistHor;
                        }
                }

                double HexDist
                {
                        get
                        {
                                hexDist = new double ();
                                hexDist = 2 * HexDistHor;
                                return hexDist;
                        }
                }

                double PolyDist
                {
                        get
                        {
                                polyDist = new double ();
                                switch (this.Topic.Style.Shape) {
                                case TopicShape.Octagon:
                                if (this.Topic.Style.PolyDistance > 0.25 * this.Height)
                                        polyDist = 0.25 * this.Height;
                                else
                                        polyDist = this.Topic.Style.PolyDistance;
                                break;
                                case TopicShape.Hexagon:
                                polyDist = this.Topic.Style.PolyDistance;
                                break;
                                default:
                                polyDist = 0;
                                break;
                                }
                                return polyDist;
                        }
                }

                double Radius
                {
                        get
                        {
                                if (this.Topic.Style.Shape == TopicShape.RoundedRectangle) {
                                        radius = 6 /*this.Topic.Style.MinMargin*/;
                                }
                                else
                                        radius = 0;
                                return radius;
                        }
                }

                public Cairo.PointD Origin
                {
                        get
                        {
                                origin.X = this.Topic.Offset.X -
                                           this.Topic.Style.LeftMargin;
                                origin.Y = this.Topic.Offset.Y -
                                           this.Topic.Style.TopMargin;
                                return origin;
                        }
                }

                public Cairo.PointD Center
                {
                        get
                        {
                                center.X = this.Origin.X +
                                           this.Width / 2;
                                center.Y = this.Origin.Y +
                                           this.Height / 2;
                                return center;
                        }
                }

                public Cairo.PointD Left
                {
                        get
                        {
                                left.X = this.Origin.X - /*
                                         this.HexDistHor -
                                         this.OctDistHor -*/
                                         this.PolyDist -
                                         this.Radius;

                                if (this.Topic.Style.Shape == TopicShape.Line)
                                        left.Y = this.Origin.Y +
                                                  this.Height;
                                else
                                        left.Y = this.Origin.Y +
                                                 this.Height / 2;
                                return left;
                        }
                }

                public Cairo.PointD Right
                {
                        get
                        {
                                right.X = this.Origin.X +
                                          this.Width +/*
                                          this.HexDistHor +
                                          this.OctDistHor +*/
                                          this.PolyDist +
                                          this.Radius;

                                if (this.Topic.Style.Shape == TopicShape.Line)
                                        right.Y = this.Origin.Y +
                                                  this.Height;
                                else
                                        right.Y = this.Origin.Y +
                                                  this.Height / 2;
                                return right;
                        }
                }

                public Cairo.PointD Top
                {
                        get
                        {
                                top.X = this.Origin.X +
                                         this.Width / 2;
                                top.Y = this.Origin.Y;
                                return top;
                        }
                }

                public Cairo.PointD Bottom
                {
                        get
                        {
                                bottom.X = this.Origin.X +
                                         this.Width / 2;
                                bottom.Y = this.Origin.Y +
                                          this.Height;
                                return bottom;
                        }
                }

                public void Sketch (Cairo.Context iContext)
                {
                        context = iContext;
                        switch (this.Topic.Style.Shape) {
                        case TopicShape.Rectangle:
                        sketchRectangle ();
                        break;
                        case TopicShape.Line:
                        sketchLine ();
                        break;
                        case TopicShape.RoundedRectangle:
                        sketchRoundedRectangle ();
                        break;
                        case TopicShape.Hexagon:
                        sketchHexagon ();
                        break;
                        case TopicShape.Octagon:
                        sketchOctagon ();
                        break;
                        case TopicShape.None:
                        break;
                        default:
                        sketchRectangle ();
                        break;
                        }
                }

                void sketchRectangle ()
                {
                        context.Rectangle (Origin, Width, Height);
                }

                void sketchLine ()
                {
                        context.MoveTo (Origin);
                        context.RelMoveTo (0, Height);
                        context.RelLineTo (Width, 0);
                }

                void sketchRoundedRectangle ()
                {
                        context.MoveTo (Origin);
                        context.Arc ((Origin.X + Width), (Origin.Y + Radius), Radius, angle4, angle1);
                        context.Arc ((Origin.X + Width), (Origin.Y + Height - Radius), Radius, angle1, angle2);
                        context.Arc (Origin.X, (Origin.Y + Height - Radius), Radius, angle2, angle3);
                        context.Arc (Origin.X, (Origin.Y + Radius), Radius, angle3, angle4);
                        context.ClosePath ();
                }

                void sketchOctagon ()
                {
                        context.MoveTo (Origin);
                        context.RelLineTo (this.Width, 0);
                        context.RelLineTo (this.PolyDist, this.PolyDist);
                        context.RelLineTo (0, this.Height - (2 * this.PolyDist));
                        context.RelLineTo (-this.PolyDist, this.PolyDist);
                        context.RelLineTo (-this.Width, 0);
                        context.RelLineTo (-this.PolyDist, -this.PolyDist);
                        context.RelLineTo (0, -(this.Height - (2 * this.PolyDist)));
                        context.ClosePath ();
                }

                void sketchHexagon ()
                {
                        context.MoveTo (Origin);
                        context.RelLineTo (this.Width, 0);
                        context.RelLineTo (this.PolyDist, this.Height / 2);
                        context.RelLineTo (-this.PolyDist, this.Height / 2);
                        context.RelLineTo (-this.Width, 0);
                        context.RelLineTo (-this.PolyDist, -this.Height / 2);
                        context.ClosePath ();
                }
        }
}