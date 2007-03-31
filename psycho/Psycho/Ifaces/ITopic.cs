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
using System.Collections.Generic;

namespace Psycho
{

        public interface ITopic
        {
                Topics Subtopics { get; }
                string Text { get; set; }

                //public Pango.Layout TextLayout { get; set; }

                int TextWidth { get;}
                int TextHeight { get;}
                Title TopicTitle { get; set; }
                TopicType Type { get; set; }
                Topic Parent { get; set; }
                string GUID { get; }
                TopicNotes Notes { get; set; }
                bool IsExpanded { get; set; }
                TopicStyle Style { get; set; }
                bool HasNotes { get;}
                int TotalCount { get; }
                string Path { get; }
                string Number { get; }
                int Level { get; }

                void AddSubtopic (Topic iTopic);
                void AddSubtopic (int iIndex, Topic iTopic);
                void Delete ();
                void ForEach (Action<Topic> action);
        }
}
