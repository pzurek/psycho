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

namespace Psycho
{
        using System;
        using Psycho;

        public class TopicOffset
        {
                double x;
                double y;
                bool isAuto;
                bool isXValid, isYValid;
                Topic topic;

                public TopicOffset (Topic iTopic)
                {
                        this.topic = iTopic;
                        this.isAuto = true;
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public double X
                {
                        get
                        {
                                if (!IsXValid) {
                                        if (this.Topic.IsCentral)
                                                x = 20;
                                        else
                                                x = this.Topic.Parent.Offset.X +
                                                    this.Topic.Parent.Frame.Width +
                                                    this.Topic.Style.HorChildDist;
                                        IsXValid = true;
                                        return x;
                                }
                                else
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
                                if (!IsYValid) {
                                        if (this.Topic.IsCentral)
                                                y = 20;
                                        else
                                                if (this.Topic.IsFirst)
                                                        y = this.Topic.Parent.Offset.Y;
                                                else
                                                        y = this.Topic.Previous.Offset.Y +
                                                            this.Topic.Previous.TotalHeight;
                                        IsYValid = true;
                                        return y;
                                }
                                else
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

                public bool IsXValid
                {
                        get { return isXValid; }
                        set { isXValid = value; }
                }

                public bool IsYValid
                {
                        get { return isYValid; }
                        set { isYValid = value; }
                }
        }
}