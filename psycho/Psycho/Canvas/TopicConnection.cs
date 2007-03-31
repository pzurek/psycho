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

namespace Psycho
{
        public class TopicConnection
        {
                Topic topic;
                ConnectionShape shape;
                Cairo.Context context;
                Cairo.Distance connectionVector;
                Cairo.PointD start, end;
                Cairo.PointD middleStart, middleEnd;
                Cairo.PointD roundedCrankCenterStart, roundedCrankCenterEnd;
                Cairo.PointD chamferedCrankStart1, chamferedCrankStart2;
                Cairo.PointD chamferedCrankEnd1, chamferedCrankEnd2;
                Cairo.PointD arcControlStart, arcControlEnd;
                Cairo.PointD curveControlStart, curveControlEnd;
                Cairo.PointD roundedAngleCrankStart, roundedAngleCrankEnd;
                double crankRadius, crankChamfer;

                static double PI = System.Math.PI;
                static double angle1 = 0.0 * (PI / 180.0);
                static double angle2 = 90.0 * (PI / 180.0);
                static double angle3 = 180.0 * (PI / 180.0);
                static double angle4 = 270.0 * (PI / 180.0);

                public TopicConnection (Topic iTopic)
                {
                        this.topic = iTopic;
                        this.shape = iTopic.Style.ConnectShape;
                        this.Start = iTopic.Frame.Left;
                        this.End = iTopic.Parent.Frame.Right;
                        this.connectionVector.Dx = this.End.X - this.Start.X;
                        this.connectionVector.Dy = this.End.Y - this.Start.Y;
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public Cairo.PointD Start
                {
                        get
                        {
                                return start;
                        }
                        set
                        {
                                start = value;
                        }
                }

                public Cairo.PointD End
                {
                        get
                        {
                                return end;
                        }
                        set
                        {
                                end = value;
                        }
                }

                public Cairo.Distance ConnectionVector
                {
                        get
                        {
                                return connectionVector;
                        }
                }


                public double CrankRadius
                {
                        get
                        {
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
                                middleStart.X = this.Start.X + this.ConnectionVector.Dx / 2;
                                middleStart.Y = this.Start.Y;
                                return middleStart;
                        }
                }

                public Cairo.PointD MiddleEnd
                {
                        get
                        {
                                middleEnd.X = this.End.X - this.ConnectionVector.Dx / 2;
                                middleEnd.Y = this.End.Y;
                                return middleEnd;
                        }
                }

                public Cairo.PointD CurveControlStart
                {
                        get
                        {
                                curveControlStart.X = this.Start.X + ConnectionVector.Dx / 1.61;
                                curveControlStart.Y = this.Start.Y;
                                return curveControlStart;
                        }
                }

                public Cairo.PointD CurveControlEnd
                {
                        get
                        {
                                curveControlEnd.X = this.End.X - ConnectionVector.Dx / 1.61;
                                curveControlEnd.Y = this.End.Y;
                                return curveControlEnd;
                        }
                }

                public Cairo.PointD RoundedCrankCenterStart
                {
                        get
                        {
                                if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                        if (this.ConnectionVector.Dx > 0) {
                                                roundedCrankCenterStart.X = MiddleStart.X - CrankRadius;
                                                roundedCrankCenterStart.Y = MiddleStart.Y + CrankRadius;
                                        }
                                        else {
                                                roundedCrankCenterStart.X = MiddleStart.X + CrankRadius;
                                                roundedCrankCenterStart.Y = MiddleStart.Y - CrankRadius;
                                        }
                                }
                                else {
                                        if (this.ConnectionVector.Dx < 0) {
                                                roundedCrankCenterStart.X = MiddleStart.X + CrankRadius;
                                                roundedCrankCenterStart.Y = MiddleStart.Y + CrankRadius;
                                        }
                                        else {
                                                roundedCrankCenterStart.X = MiddleStart.X - CrankRadius;
                                                roundedCrankCenterStart.Y = MiddleStart.Y - CrankRadius;
                                        }
                                }
                                return roundedCrankCenterStart;
                        }
                }

                public Cairo.PointD RoundedCrankCenterEnd
                {
                        get
                        {
                                if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                        if (this.ConnectionVector.Dx > 0) {
                                                roundedCrankCenterEnd.X = MiddleEnd.X + CrankRadius;
                                                roundedCrankCenterEnd.Y = MiddleEnd.Y - CrankRadius;
                                        }
                                        else {
                                                roundedCrankCenterEnd.X = MiddleEnd.X - CrankRadius;
                                                roundedCrankCenterEnd.Y = MiddleEnd.Y + CrankRadius;
                                        }
                                }
                                else {
                                        if (this.ConnectionVector.Dx < 0) {
                                                roundedCrankCenterEnd.X = MiddleEnd.X - CrankRadius;
                                                roundedCrankCenterEnd.Y = MiddleEnd.Y - CrankRadius;
                                        }
                                        else {
                                                roundedCrankCenterEnd.X = MiddleEnd.X + CrankRadius;
                                                roundedCrankCenterEnd.Y = MiddleEnd.Y + CrankRadius;
                                        }
                                } return roundedCrankCenterEnd;
                        }
                }

                public Cairo.PointD ChamferedCrankStart1
                {
                        get
                        {
                                chamferedCrankStart1.X = this.MiddleStart.X + this.CrankRadius;
                                chamferedCrankStart1.Y = this.MiddleStart.Y;
                                return chamferedCrankStart1;
                        }
                }

                public Cairo.PointD ChamferedCrankStart2
                {
                        get
                        {
                                chamferedCrankStart2.X = this.MiddleStart.X;
                                chamferedCrankStart2.Y = this.MiddleStart.Y - this.CrankRadius;
                                return chamferedCrankStart2;
                        }
                }

                public Cairo.PointD ChamferedCrankEnd1
                {
                        get
                        {
                                chamferedCrankEnd1.X = this.MiddleEnd.X - this.CrankChamfer;
                                chamferedCrankEnd1.Y = this.MiddleEnd.Y;
                                return chamferedCrankEnd1;
                        }
                }

                public Cairo.PointD ChamferedCrankEnd2
                {
                        get
                        {
                                chamferedCrankEnd2.X = this.MiddleEnd.X;
                                chamferedCrankEnd2.Y = this.MiddleEnd.Y + this.CrankChamfer;
                                return chamferedCrankEnd2;
                        }
                }

                public Cairo.PointD ArcControlStart
                {
                        get
                        {
                                arcControlStart.X = this.Start.X + this.ConnectionVector.Dx / 2;
                                arcControlStart.Y = this.Start.Y;
                                return arcControlStart;
                        }
                }

                public Cairo.PointD ArcControlEnd
                {
                        get
                        {
                                arcControlEnd.X = this.End.X;
                                arcControlEnd.Y = this.End.Y - this.ConnectionVector.Dy / 2;
                                return arcControlEnd;
                        }
                }

                public Cairo.PointD RoundedAngleCrankStart
                {
                        get
                        {
                                roundedAngleCrankStart.X = this.Start.X + this.ConnectionVector.Dx / 4;
                                roundedAngleCrankStart.Y = this.Start.Y;
                                return roundedAngleCrankStart;
                        }
                }

                public Cairo.PointD RoundedAngleCrankEnd
                {
                        get
                        {
                                roundedAngleCrankEnd.X = this.End.X - this.ConnectionVector.Dx / 4;
                                roundedAngleCrankEnd.Y = this.End.Y - this.ConnectionVector.Dy / 2;
                                return roundedAngleCrankEnd;
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
                        case ConnectionShape.AngleCrank:
                        sketchAngleCrank ();
                        break;
                        case ConnectionShape.RoundedAngleCrank:
                        sketchRoundedAngleCrank ();
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
                        if (this.ConnectionVector.Dx * this.ConnectionVector.Dy > 0) {
                                if (this.ConnectionVector.Dx > 0) {
                                        context.Arc (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle4, angle1);
                                        context.ArcNegative (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle3, angle2);
                                }
                                else {
                                        context.Arc (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle2, angle3);
                                        context.ArcNegative (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle1, angle4);
                                }
                        }
                        else {
                                if (this.ConnectionVector.Dx > 0) {
                                        context.ArcNegative (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle2, angle1);
                                        context.Arc (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle3, angle4);
                                }
                                else {
                                        context.ArcNegative (RoundedCrankCenterStart.X, RoundedCrankCenterStart.Y, this.CrankRadius, angle4, angle3);
                                        context.Arc (RoundedCrankCenterEnd.X, RoundedCrankCenterEnd.Y, this.CrankRadius, angle1, angle2);
                                }
                        }
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

                private void sketchAngleCrank ()
                {
                        context.MoveTo (Start);
                        context.LineTo (MiddleStart);
                        context.LineTo (End);
                }

                private void sketchRoundedAngleCrank ()
                {
                        context.MoveTo (Start);
                        context.LineTo (RoundedAngleCrankStart);
                        context.CurveTo (MiddleStart, MiddleStart, RoundedAngleCrankEnd);
                        context.LineTo (End);
                }
        }
}