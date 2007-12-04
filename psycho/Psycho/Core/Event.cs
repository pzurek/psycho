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
        using System.Collections;
        using System.Collections.Generic;
        using Psycho;

        public class Event
        {
                /// <summary>
                /// Event object holds data that transforms a Topic into a calendar element.
                /// </summary>
                /// <param name="iTopic"></param>
                public Event (Topic iTopic)
                {
                        this.topic = iTopic;
                }

                Topic topic;

                string subject;

                public string Subject
                {
                        get { return subject; }
                        set { subject = value; }
                }
                DateTime startDate;
                DateTime dueDate;
                Priority priority;
                Status status;
                int completeness;
                string recurrence;
                string notes;

                public DateTime StartDate
                {
                        get { return startDate; }
                        set { startDate = value; }
                }

                public DateTime DueDate
                {
                        get { return dueDate; }
                        set { dueDate = value; }
                }

                public Topic Topic
                {
                        get { return topic; }
                }

                public string GUID
                {
                        get { return this.Topic.GUID; }
                }

                public Priority Priority
                {
                        get { return priority; }
                        set { priority = value; }
                }

                public Status Status
                {
                        get { return status; }
                        set { status = value; }
                }

                public int Completeness
                {
                        get { return completeness; }
                        set { completeness = value; }
                }

                public string Recurrence
                {
                        get { return recurrence; }
                        set { recurrence = value; }
                }

                public String Notes
                {
                        get { return notes; }
                        set { notes = value; }
                }
        }
}