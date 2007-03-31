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
                Topic topic;

                public TopicOffset (Topic iTopic)
                {
                        this.topic = iTopic;
                        this.isAuto = true;
                }

                public double X
                {
                        get
                        {
                                if (isAuto) {
                                        if (topic.IsCentral)
                                                x = 10;
                                        else
                                                x = topic.Parent.Offset.X + topic.Parent.Frame.Width + 49;
                                        return x;
                                }
                                else
                                        return x;
                        }
                        set
                        {
                                isAuto = false;
                                x = value;
                        }
                }

                public double Y
                {
                        get
                        {
                                if (isAuto) {
                                        if (this.topic.IsCentral)
                                                y = 10;
                                        else
                                                if (this.topic.IsFirst)
                                                        y = this.topic.Parent.Offset.Y;
                                                else
                                                        y = this.topic.Previous.Offset.Y +
                                                            this.topic.Previous.TotalHeight /*+
                                                            this.topic.Style.Padding*/
                                                                                      ;
                                        return y;
                                }
                                else
                                        return y;
                        }
                        set
                        {
                                isAuto = false;
                                y = value;
                        }
                }

                public void SetOffset (int iXOffset, int iYOffset)
                {
                        isAuto = false;
                        x = iXOffset;
                        y = iYOffset;
                }

                public void GetOffset (out double outXOffset, out double outYOffset)
                {
                        outXOffset = x;
                        outYOffset = y;
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public bool IsAuto
                {
                        get { return isAuto; }
                        set { isAuto = value; }
                }
        }
}