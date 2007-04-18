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
using System.Collections.Generic;

namespace Psycho
{
        public class TopicList : List<Topic>
        {
                double height;
                double width;
                Topic topic;
                Topic first;
                Topic last;
                TopicListOrientation orientation;

                public double Height
                {
                        get
                        {
                                height = 0;
                                if (this.Count > 0) {
                                        if (this.Orientation == TopicListOrientation.Vertical) {
                                                foreach (Topic child in this)
                                                        height += child.TotalHeight;
                                        }
                                        else
                                                foreach (Topic child in this)
                                                        if (child.TotalHeight > height)
                                                                height = child.TotalHeight;
                                }
                                return height;
                        }
                }

                public double Width
                {
                        get
                        {
                                width = 0;
                                if (this.Count > 0) {
                                        if (this.Orientation == TopicListOrientation.Horizontal) {
                                                foreach (Topic child in this)
                                                        width += child.TotalWidth;
                                        }
                                        else
                                                foreach (Topic child in this)
                                                        if (child.TotalWidth > width)
                                                                width = child.TotalWidth;
                                }
                                return width;
                        }
                }

                public TopicList (Topic iTopic)
                        : base ()
                {
                        this.topic = iTopic;
                }

                public TopicList ()
                        : base ()
                {
                }

                public Topic Parent
                {
                        get { return topic; }
                }

                public TopicListOrientation Orientation
                {
                        get
                        {
                                switch (this.Parent.Style.SubLayout) {
                                case SubtopicLayout.OrgChart:
                                orientation = TopicListOrientation.Horizontal;
                                break;
                                case SubtopicLayout.Map:
                                orientation = TopicListOrientation.Vertical;
                                break;
                                case SubtopicLayout.Tree:
                                orientation = TopicListOrientation.Vertical;
                                break;
                                }
                                return orientation;
                        }
                }

                public Topic First
                {
                        get
                        {
                                if (this.Count > 0)
                                        first = this[0];
                                return first;
                        }
                }

                public Topic Last
                {
                        get
                        {
                                if (this.Count > 0)
                                        last = this[this.Count];
                                return last;
                        }
                }
        }
}