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
        public class TopicStyle
        {
                Topic topic;
                Font styleFont;
                TopicShape shape;
                ConnectionPoint connectPoint;
                ConnectionShape connectShape;
                Color fillColor;
                Color strokeColor;
                double strokeWidth;
                bool equalMargins;
                int leftMargin;
                int rightMargin;
                int topMargin;
                int bottomMargin;
                int minMargin;
                int maxMargin;
                int padding, horChildDist, verChildDist, orgChartVertDist;
                int crankRadius, crankChamfer;
                int polyDistance;
                bool fixedWidth;
                int width;
                SubtopicLayout subLayout;

                Stylus stylus = new Stylus ();

                public TopicStyle (Topic iTopic) //TODO: One big hack
                {
                        this.topic = iTopic;

                        // TODO: That all of course has to be loaded from a style
                        this.StyleFont = (new Font ("Tahoma", 10));
                        this.StrokeWidth = 1;
                        this.EqualMargins = true;
                        this.LeftMargin = 2;
                        this.RightMargin = 0;
                        this.TopMargin = 0;
                        this.BottomMargin = 0;
                        this.CrankChamfer = 7;
                        this.CrankRadius = 7;
                        this.PolyDistance = 7;
                        this.Width = 140;
                        this.Padding = 5;
                        this.HorChildDist = 56;
                        this.VerChildDist = 14;
                        this.OrgChartVertDist = 49;
                        this.ConnectPoint = ConnectionPoint.Edge;
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public Font StyleFont
                {
                        get { return styleFont; }
                        set { styleFont = value; }
                }

                public TopicShape Shape
                {
                        get
                        {
                                switch (this.Topic.Level) {
                                        case 0:
                                        shape = TopicShape.RoundedRectangle;
                                        break;
                                        case 1:
                                        shape = TopicShape.Hexagon;
                                        break;
                                        case 2:
                                        shape = TopicShape.Octagon;
                                        break;
                                        case 3:
                                        shape = TopicShape.Rectangle;
                                        break;
                                        default:
                                        shape = TopicShape.Hexagon;
                                        break;
                                }
                                return shape;
                        }
                        set { shape = value; }
                }

                public ConnectionPoint ConnectPoint
                {
                        get { return connectPoint; }
                        set { connectPoint = value; }
                }

                public SubtopicLayout SubLayout
                {
                        get
                        {
                                switch (this.Topic.Index) {
                                        case 0:
                                        subLayout = SubtopicLayout.Map;
                                        break;
                                        case 1:
                                        subLayout = SubtopicLayout.Tree;
                                        break;
                                        case 2:
                                        subLayout = SubtopicLayout.OrgChart;
                                        break;
                                        default:
                                        subLayout = SubtopicLayout.Map;
                                        break;
                                }

                                return subLayout;
                        }
                        set { subLayout = value; }
                }

                public ConnectionShape ConnectShape
                {
                        get
                        {
                                switch (this.Topic.Level) {
                                        case 0:
                                        connectShape = ConnectionShape.Curve;
                                        break;
                                        case 1:
                                        connectShape = ConnectionShape.RoundedCrank;
                                        break;
                                        case 2:
                                        connectShape = ConnectionShape.Arc;
                                        break;
                                        case 3:
                                        connectShape = ConnectionShape.RoundedAngleCrank;
                                        break;
                                        case 4:
                                        connectShape = ConnectionShape.AngleCrank;
                                        break;
                                        case 5:
                                        connectShape = ConnectionShape.ChamferedCrank;
                                        break;
                                        case 6:
                                        connectShape = ConnectionShape.Crank;
                                        break;
                                        default:
                                        connectShape = ConnectionShape.Straight;
                                        break;
                                }
                                return connectShape;
                        }
                        set { connectShape = value; }
                }

                public Color FillColor
                {
                        get { return fillColor; }
                        set { fillColor = value; }
                }

                public Color StrokeColor
                {
                        get
                        {
                                if (strokeColor == null) {
                                        if (this.Topic.Level < 2) {
                                                switch (this.Topic.Number) {
                                                        case "0":
                                                        strokeColor = new Color (55, 55, 55);
                                                        break;
                                                        case "1":
                                                        strokeColor = new Color (52, 101, 164);// (239, 41, 41);
                                                        break;
                                                        case "2":
                                                        strokeColor = new Color (239, 41, 41);// (237, 212, 0);
                                                        break;
                                                        case "3":
                                                        strokeColor = new Color (237, 212, 0);
                                                        break;
                                                        case "4":
                                                        strokeColor = new Color (115, 210, 22);
                                                        break;
                                                        case "5":
                                                        strokeColor = new Color (173, 127, 168);
                                                        break;
                                                        case "6":
                                                        strokeColor = new Color (245, 121, 0);
                                                        break;
                                                        default:
                                                        strokeColor = new Color (32, 74, 135);
                                                        break;
                                                }
                                        }
                                        else
                                                strokeColor = this.Topic.Parent.Style.StrokeColor;
                                }
                                return strokeColor;
                        }
                        set { strokeColor = value; }
                }

                public double StrokeWidth
                {
                        get
                        {
                                switch (this.Topic.Level) {
                                        case 0:
                                        strokeWidth = 5;
                                        break;
                                        case 1:
                                        strokeWidth = 4;
                                        break;
                                        case 2:
                                        strokeWidth = 3;
                                        break;
                                        default:
                                        strokeWidth = 2;
                                        break;
                                }
                                return strokeWidth;
                        }
                        set { strokeWidth = value; }
                }

                public bool EqualMargins
                {
                        get { return equalMargins; }
                        set { equalMargins = value; }
                }

                public int LeftMargin
                {
                        get { return leftMargin; }
                        set { leftMargin = value; }
                }

                public int RightMargin
                {
                        get
                        {
                                if (equalMargins)
                                        return leftMargin;
                                else
                                        return rightMargin;
                        }
                        set { rightMargin = value; }
                }

                public int TopMargin
                {
                        get
                        {
                                if (equalMargins)
                                        return leftMargin;
                                else
                                        return topMargin;
                        }
                        set { topMargin = value; }
                }

                public int BottomMargin
                {
                        get
                        {
                                if (equalMargins)
                                        return leftMargin;
                                else
                                        return bottomMargin;
                        }
                        set { bottomMargin = value; }
                }

                public int MinMargin
                {
                        get
                        {
                                minMargin = LeftMargin;
                                if (RightMargin < minMargin)
                                        minMargin = RightMargin;
                                if (TopMargin < minMargin)
                                        minMargin = TopMargin;
                                if (BottomMargin < minMargin)
                                        minMargin = BottomMargin;
                                return minMargin;
                        }
                }

                public int MaxMargin
                {
                        get
                        {
                                maxMargin = LeftMargin;
                                if (RightMargin > minMargin)
                                        maxMargin = RightMargin;
                                if (TopMargin > minMargin)
                                        maxMargin = TopMargin;
                                if (BottomMargin > minMargin)
                                        maxMargin = BottomMargin;
                                return maxMargin;
                        }
                }

                public int CrankRadius
                {
                        get { return crankRadius; }
                        set { crankRadius = value; }
                }

                public int CrankChamfer
                {
                        get { return crankChamfer; }
                        set { crankChamfer = value; }
                }

                public int PolyDistance
                {
                        get { return polyDistance; }
                        set { polyDistance = value; }
                }

                public int Padding
                {
                        get { return padding; }
                        set { padding = value; }
                }

                public int HorChildDist
                {
                        get { return horChildDist; }
                        set { horChildDist = value; }
                }

                public int VerChildDist
                {
                        get { return verChildDist; }
                        set { verChildDist = value; }
                }

                public int OrgChartVertDist
                {
                        get { return orgChartVertDist; }
                        set { orgChartVertDist = value; }
                }

                public bool FixedWidth
                {
                        get { return fixedWidth; }
                        set { fixedWidth = value; }
                }

                public int Width
                {
                        get { return width; }
                        set { width = value; }
                }
        }
}