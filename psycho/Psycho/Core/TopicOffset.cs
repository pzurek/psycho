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

namespace Psycho
{
        using System;
        using Psycho;

        public class TopicOffset
        {
                double x;
                double y;
                bool isAuto;
                Topic topic;

                public TopicOffset (Topic iTopic)
                {
                        this.topic = iTopic;
                        this.isAuto = true;
                        this.Update (iTopic);
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public void Update (Topic iTopic)
                {
                        if (iTopic.IsCentral)
                                x = 10 /*- iTopic.TextHeight / 2*/;
                        else
                                x = System.Math.Floor (iTopic.Parent.Offset.X +
                                    iTopic.Parent.Frame.Width +
                                    iTopic.Style.HorChildDist);

                        if (iTopic.IsCentral)
                                y = 10 /*- iTopic.TextWidth / 2*/;
                        else
                                if (iTopic.IsFirst)
                                        y = System.Math.Floor (iTopic.Parent.Offset.Y);
                                else
                                        y = System.Math.Floor (iTopic.Previous.Offset.Y +
                                            iTopic.Previous.TotalHeight);
                }

                public double X
                {
                        get
                        {
                                return x;
                        }
                        set
                        {
                                IsAuto = false;
                                x = value;
                        }
                }

                public double Y
                {
                        get
                        {
                                return y;
                        }
                        set
                        {
                                IsAuto = false;
                                y = value;
                        }
                }

                public void SetOffset (int iXOffset, int iYOffset)
                {
                        isAuto = false;
                        X = iXOffset;
                        Y = iYOffset;
                }

                public void GetOffset (out double outXOffset, out double outYOffset)
                {
                        outXOffset = X;
                        outYOffset = Y;
                }

                public bool IsAuto
                {
                        get { return isAuto; }
                        set { isAuto = value; }
                }
        }
}