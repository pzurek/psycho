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
                int textWidth, textHeight, frameHeight, frameWidth;
                int leftMargin, rightMargin, topMargin, bottomMargin;
                int leftOverhead, rightOverhead;
                Cairo.PointD origin, center, left, right, top, bottom;
                double octDist, octDistOrtho;
                double hexDist, hexDistOrtho;
                Topic topic;

                public int Width
                {
                        get
                        {
                                frameWidth = this.topic.TextWidth +
                                             this.topic.Style.LeftMargin +
                                             this.topic.Style.RightMargin;
                                return frameWidth;
                        }
                }

                public int Height
                {
                        get
                        {
                                frameHeight = this.topic.TextHeight +
                                             this.topic.Style.TopMargin +
                                             this.topic.Style.BottomMargin;
                                return frameHeight;
                        }
                }

                double OctDist (int iHeight)
                {
                        octDist = new double ();
                        octDist = iHeight / (System.Math.Sqrt (2) + 1);
                        return octDist;
                }

                double OctDistOrtho (int iHeight)
                {
                        octDistOrtho = new double ();
                        octDistOrtho = octDist / (System.Math.Sqrt (2));
                        return octDistOrtho;
                }

                public TopicFrame (Topic iTopic)
                {
                        this.topic = iTopic;
                }

                public Cairo.PointD Origin
                {
                        get
                        {
                                origin.X = this.topic.Offset.X -
                                           this.topic.Style.LeftMargin;
                                origin.Y = this.topic.Offset.Y -
                                           this.topic.Style.TopMargin;
                                return origin;
                        }
                }

                public Cairo.PointD Center
                {
                        get
                        {
                                center.X = this.topic.Offset.X +
                                           this.topic.TextWidth / 2;
                                center.Y = this.topic.Offset.Y +
                                           this.topic.TextHeight / 2;
                                return center;
                        }
                }

                public Cairo.PointD Left
                {
                        get
                        {
                                left.X = this.topic.Offset.X -
                                         this.topic.Style.LeftMargin;
                                left.Y = this.topic.Offset.Y +
                                         this.topic.TextHeight / 2;
                                return left;
                        }
                }

                public Cairo.PointD Right
                {
                        get
                        {
                                right.X = this.topic.Offset.X +
                                          this.topic.TextWidth +
                                          this.topic.Style.RightMargin;
                                right.Y = this.topic.Offset.Y +
                                          this.topic.TextHeight / 2;
                                return right;
                        }
                }

                public Cairo.PointD Top
                {
                        get
                        {
                                top.X = this.topic.Offset.X +
                                         this.topic.TextWidth / 2;
                                top.Y = this.topic.Offset.Y -
                                        this.topic.Style.TopMargin;
                                return top;
                        }
                }

                public Cairo.PointD Bottom
                {
                        get
                        {
                                bottom.X = this.topic.Offset.X +
                                         this.topic.TextWidth / 2;
                                bottom.Y = this.topic.Offset.Y +
                                          this.topic.TextHeight +
                                           this.topic.Style.BottomMargin;
                                return bottom;
                        }
                }

                public void Draw (Cairo.Context cr, Topic iTopic)
                {
                        switch (this.topic.Style.Shape) {
                        case TopicShape.Rectangle:
                        DrawRectangle (cr, iTopic);
                        break;
                        default:
                        DrawRectangle (cr, iTopic);
                        break;
                        }
                }

                public void DrawRectangle (Cairo.Context cr, Topic iTopic)
                {
                        cr.Rectangle (Origin, Width, Height);
                        Cairo.Color strokeColor;
                        if (iTopic.IsCurrent)
                                strokeColor = new Cairo.Color (1, 0, 0);
                        else
                                strokeColor = new Cairo.Color (0, 0, 1);
                        cr.Color = strokeColor;
                        cr.LineWidth = iTopic.Style.StrokeWidth;
                        cr.Stroke ();
                }

                public void DrawLine (Cairo.PointD origin, int width, int height)
                {
                        context.MoveTo (origin);
                }

                public void DrawRoundedRectangle (Cairo.PointD origin, int width, int height)
                {
                        context.MoveTo (origin);
                }

                public void DrawOctagon (Cairo.PointD origin, int width, int height)
                {
                        context.MoveTo (origin);
                }

                public void DrawHexagon (Cairo.PointD origin, int width, int height)
                {
                        context.MoveTo (origin);
                }
        }
}