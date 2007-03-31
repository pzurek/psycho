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

namespace Psycho
{
        using System;
        using Psycho;

        public class TopicOffset
        {
                public TopicOffset (Topic paramTopic)
                {
                        this.topic = paramTopic;
                        this.isAuto = true;
                }

                int x;
                int y;
                bool isAuto;
                Topic topic;

                public int X
                {
                        get
                        {
                                if (isAuto) {
                                        /*some elaborate calculations*/
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

                public int Y
                {
                        get
                        {
                                if (isAuto) {
                                        /*some elaborate calculations*/
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

                public void SetOffset (int paramXOffset, int paramYOffset)
                {
                        isAuto = false;
                        x = paramXOffset;
                        y = paramYOffset;
                }

                public void GetOffset (out int outXOffset, out int outYOffset)
                {
                        outXOffset = x;
                        outYOffset = y;
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public string GUID
                {
                        get { return this.topic.GUID; }
                }

                public bool IsAuto
                {
                        get { return isAuto; }
                        set { isAuto = value; }
                }
        }
}
