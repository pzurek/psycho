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
                Cairo.Distance connectionVector;
                Cairo.PointD start, end;
                Cairo.PointD middleStart, middleEnd;
                Cairo.PointD roundedCrankCenterStart, roundedCrankCenterEnd;
                Cairo.PointD chamferedCrankStart1, chamferedCrankStart2;
                Cairo.PointD chamferedCrankEnd1, chamferedCrankEnd2;
                Cairo.PointD arcControlStart, arcControlEnd;
                Cairo.PointD curveControlStart, curveControlEnd;
                double crankRadius, crankChamfer;

                static double PI = System.Math.PI;
                static double angle1 = 0.0 * (PI / 180.0);
                static double angle2 = 90.0 * (PI / 180.0);
                static double angle3 = 180.0 * (PI / 180.0);
                static double angle4 = 270.0 * (PI / 180.0);

                public TopicConnection (Topic iTopic)
                {
                        this.topic = iTopic;
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public Cairo.Distance ConnectionVector
                {
                        get
                        {
                                connectionVector = new Cairo.Distance ();
                                connectionVector.Dx = this.Start.X - this.End.X;
                                connectionVector.Dy = this.Start.Y - this.End.Y;
                                return connectionVector;
                        }
                }

                public Cairo.PointD Start
                {
                        get
                        {
                                start = this.Topic.Frame.Left;
                                return start;
                        }
                }

                public Cairo.PointD End
                {
                        get
                        {
                                end = this.Topic.Parent.Frame.Right;
                                return end;
                        }
                }

                public double CrankRadius
                {
                        get
                        {
                                crankRadius = 0;
                                if (System.Math.Abs (this.ConnectionVector.Dy) > (2 * this.Topic.Style.CrankRadius))
                                        crankRadius = this.Topic.Style.CrankRadius;
                                else
                                        crankRadius = System.Math.Abs (this.ConnectionVector.Dy) / 2;
                                return crankRadius;
                        }
                }

                public double CrankChamfer
                {
                        get
                        {
                                crankChamfer = 0;
                                if (System.Math.Abs (this.ConnectionVector.Dy) > (2 * this.Topic.Style.CrankChamfer))
                                        crankChamfer = this.Topic.Style.CrankChamfer;
                                else
                                        crankChamfer = System.Math.Abs (this.ConnectionVector.Dy) / 2;
                                return crankChamfer;
                        }
                }

                public Cairo.PointD MiddleStart
                {
                        get
                        {
                                middleStart = new Cairo.PointD ();
                                middleStart.X = this.Topic.Parent.Frame.Right.X + ((
                                                this.Topic.Frame.Left.X -
                                                this.Topic.Parent.Frame.Right.X) / 2);
                                middleStart.Y = this.Topic.Frame.Left.Y;
                                return middleStart;
                        }
                }

                public Cairo.PointD MiddleEnd
                {
                        get
                        {
                                middleEnd = new Cairo.PointD ();
                                middleEnd.X = this.Topic.Parent.Frame.Right.X + ((
                                              this.Topic.Frame.Left.X -
                                              this.Topic.Parent.Frame.Right.X) / 2);
                                middleEnd.Y = this.Topic.Parent.Frame.Right.Y;
                                return middleEnd;
                        }
                }

                public Cairo.PointD CurveControlStart
                {
                        get
                        {
                                curveControlStart = new Cairo.PointD ();
                                curveControlStart.X = this.Topic.Frame.Left.X - ConnectionVector.Dx / 1.61;
                                curveControlStart.Y = this.Topic.Frame.Left.Y;
                                return curveControlStart;
                        }
                }

                public Cairo.PointD CurveControlEnd
                {
                        get
                        {
                                curveControlEnd = new Cairo.PointD ();
                                curveControlEnd.X = this.Topic.Parent.Frame.Right.X + ConnectionVector.Dx / 1.61;
                                curveControlEnd.Y = this.Topic.Parent.Frame.Right.Y;
                                return curveControlEnd;
                        }
                }

                public Cairo.PointD RoundedCrankCenterStart
                {
                        get
                        {
                                roundedCrankCenterStart = new Cairo.PointD ();
                                roundedCrankCenterStart.X = this.MiddleStart.X + this.CrankRadius;
                                roundedCrankCenterStart.Y = this.MiddleStart.Y - this.CrankRadius;
                                return roundedCrankCenterStart;
                        }
                }

                public Cairo.PointD RoundedCrankCenterEnd
                {
                        get
                        {
                                roundedCrankCenterEnd = new Cairo.PointD ();
                                roundedCrankCenterEnd.X = this.MiddleEnd.X - this.CrankRadius;
                                roundedCrankCenterEnd.Y = this.MiddleEnd.Y + this.CrankRadius;
                                return roundedCrankCenterEnd;
                        }
                }

                public Cairo.PointD ChamferedCrankStart1
                {
                        get
                        {
                                chamferedCrankStart1 = new Cairo.PointD ();
                                chamferedCrankStart1.X = this.MiddleStart.X + this.CrankRadius;
                                chamferedCrankStart1.Y = this.MiddleStart.Y;
                                return chamferedCrankStart1;
                        }
                }

                public Cairo.PointD ChamferedCrankStart2
                {
                        get
                        {
                                chamferedCrankStart2 = new Cairo.PointD ();
                                chamferedCrankStart2.X = this.MiddleStart.X;
                                chamferedCrankStart2.Y = this.MiddleStart.Y - this.CrankRadius;
                                return chamferedCrankStart2;
                        }
                }

                public Cairo.PointD ChamferedCrankEnd1
                {
                        get
                        {
                                chamferedCrankEnd1 = new Cairo.PointD ();
                                chamferedCrankEnd1.X = this.MiddleEnd.X - this.CrankChamfer;
                                chamferedCrankEnd1.Y = this.MiddleEnd.Y;
                                return chamferedCrankEnd1;
                        }
                }

                public Cairo.PointD ChamferedCrankEnd2
                {
                        get
                        {
                                chamferedCrankEnd2 = new Cairo.PointD ();
                                chamferedCrankEnd2.X = this.MiddleEnd.X;
                                chamferedCrankEnd2.Y = this.MiddleEnd.Y + this.CrankChamfer;
                                return chamferedCrankEnd2;
                        }
                }

                public Cairo.PointD ArcControlStart
                {
                        get
                        {
                                arcControlStart = new Cairo.PointD ();
                                arcControlStart.X = this.Start.X - this.ConnectionVector.Dx / 2;
                                arcControlStart.Y = this.Start.Y;
                                return arcControlStart;
                        }
                }

                public Cairo.PointD ArcControlEnd
                {
                        get
                        {
                                arcControlEnd = new Cairo.PointD ();
                                arcControlEnd.X = this.End.X;
                                arcControlEnd.Y = this.End.Y + this.ConnectionVector.Dy / 2;
                                return arcControlEnd;
                        }
                }

                public void Sketch (Cairo.Context iContext)
                {
                        if (this.Topic.Parent == null)
                                return;
                        context = iContext;
                        switch (this.Topic.Parent.Style.ConnectShape) {
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
                        context.CurveTo (CurveControlStart, CurveControlEnd, End);
                }

                private void sketchArc ()
                {
                        context.MoveTo (Start);
                        context.CurveTo (ArcControlStart, ArcControlEnd, End);
                }

                private void sketchRoundedCrank ()
                {
                        context.MoveTo (Start);
                        context.Arc (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle2, angle3);
                        context.ArcNegative (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle1, angle4);
                        context.LineTo (End);
                }

                private void sketchChamferedCrank ()
                {
                        context.MoveTo (Start);
                        context.LineTo (ChamferedCrankStart1);
                        context.LineTo (ChamferedCrankStart2);
                        context.LineTo (ChamferedCrankEnd2);
                        context.LineTo (ChamferedCrankEnd1);
                        context.LineTo (End);
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