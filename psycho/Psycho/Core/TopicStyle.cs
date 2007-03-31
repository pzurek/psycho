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
using System.Text;

namespace Psycho
{
        public class TopicStyle
        {
                Font styleFont;
                TopicShape shape;
                ConnectionPoint connectPoint;
                ConnectionShape connectShape;
                SubtopicDystributionDirection subbtopicDistribution;
                Color fillColor;
                Color strokeColor;
                double strokeWidth;
                bool equalMargins;
                int leftMargin;
                int rightMargin;
                int topMargin;
                int bottomMargin;
                int padding;
                bool fixedWidth;
                int width;

                Stylus stylus = new Stylus ();

                public TopicStyle () //TODO: One big hack
                {
                        this.StyleFont = (new Font ("Bitstream Vera Sans", 10)); // TODO: That of course has to be loaded from style
                        this.Shape = TopicShape.RoundedRectangle;
                        this.ConnectPoint = ConnectionPoint.Edge;
                        this.ConnectShape = ConnectionShape.Curve;
                        Color fcolor = (stylus.GetFirstColor (out fcolor));
                        this.FillColor = fcolor;
                        FillColor.SetAlfa (20);
                        this.StrokeColor = fillColor; // TODO: For now the stroke color is the same as fill color. To be changed later
                        this.StrokeWidth = 2;
                        this.EqualMargins = true;
                        this.LeftMargin = 5;
                        this.Width = 100;
                        this.Padding = 5;
                }

                public Font StyleFont
                {
                        get { return styleFont; }
                        set { styleFont = value; }
                }

                public TopicShape Shape
                {
                        get { return shape; }
                        set { shape = value; }
                }

                public ConnectionPoint ConnectPoint
                {
                        get { return connectPoint; }
                        set { connectPoint = value; }
                }

                public ConnectionShape ConnectShape
                {
                        get { return connectShape; }
                        set { connectShape = value; }
                }

                public SubtopicDystributionDirection SubtopicDistribution
                {
                        get { return subbtopicDistribution; }
                        set { subbtopicDistribution = value; }
                }

                public Color FillColor
                {
                        get { return fillColor; }
                        set { fillColor = value; }
                }

                public Color StrokeColor
                {
                        get { return strokeColor; }
                        set { strokeColor = value; }
                }

                public double StrokeWidth
                {
                        get { return strokeWidth; }
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

                public int Padding
                {
                        get { return padding; }
                        set { padding = value; }
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
