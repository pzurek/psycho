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

namespace Psycho {
        using System;
        using System.Collections;
        using System.Collections.Generic;
        using System.Xml;
        using Psycho;

        public class Topic {

                public Topic ()
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = ("Topic ");
                        this.isExpanded = false;
                }

                public Topic (int topic_number)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = ("Topic " + topic_number.ToString ());
                        this.isExpanded = false;
                }

                private string text;
                private string number;
                private Topic parent;
                private int level;
                private int totalCount;
                private string path;
                private string guid;
                private bool isExpanded;
                private Notes topicNotes;
                private Style topicStyle;
                private Title topicTitle;
                private TopicType type;

                public Topics Subtopics = new Topics ();

                public string Text
                {
                        get { return text; }
                        set { text = value; }
                }

                public Title TopicTitle
                {
                        get { return topicTitle; }
                        set { topicTitle = value; }
                }

                public TopicType Type
                {
                        get { return type; }
                        set { type = value; }
                }

                public Topic Parent
                {
                        get { return parent; }
                        set { parent = value; }
                }

                public string GUID
                {
                        get { return guid; }
                }

                public Notes TopicNotes
                {
                        get { return topicNotes; }
                        set { topicNotes = value; }
                }

                public bool IsExpanded
                {
                        get { return isExpanded; }
                        set { isExpanded = value; }
                }

                public Style TopicStyle
                {
                        get { return topicStyle; }
                        set { topicStyle = value; }
                }

                public bool HasNotes
                {
                        get
                        {
                                if (this.topicNotes != null && this.topicNotes.Text != "")
                                        return true;
                                else
                                        return false;
                        }
                }

                public int TotalCount
                {
                        get { this.CountDescendants (); return totalCount; }
                }

                public string Path
                {
                        get
                        {
                                if (this.Parent == null)
                                        this.path = "0";
                                else
                                        this.path = (this.Parent.path + ":" + this.Parent.Subtopics.IndexOf (this).ToString ());
                                return path;
                        }
                }

                public string Number
                {
                        get {
                                if (this.Parent == null)
                                        this.number = "0";
                                else
                                        if (this.Parent.Number == "0")
                                                this.number = ((this.Parent.Subtopics.IndexOf (this) + 1).ToString ());
                                        else
                                                this.number = (this.Parent.Number.ToString () + "." + (this.Parent.Subtopics.IndexOf (this) + 1).ToString ());
                                return number;
                        }
                }

                public int Level
                {
                        get { this.CalculateLevel (); return level; }
                }

                public void AddSubtopic (Topic paramTopic)
                {
                        this.Subtopics.Add (paramTopic);
                }

                public void AddSubtopicAt (int paramIndex, Topic paramTopic)
                {
                        this.Subtopics.Insert (paramIndex, paramTopic);
                }

                public void Delete ()
                {
                        if (this.Parent != null)
                                this.Parent.Subtopics.Remove (this);
                }

                private void BuildPath ()
                {
                        if (this.Parent == null)
                                this.path = "0";
                        else
                                this.path = (this.Parent.path + ":" + this.Parent.Subtopics.IndexOf (this).ToString ());
                }

                public void ForEach (Action<Topic> action)
                {
                        Queue<Topic> remaining = new Queue<Topic> ();

                        remaining.Enqueue (this);

                        while (remaining.Count > 0) {
                                Topic topic = remaining.Dequeue ();
                                action (topic);
                                foreach (Topic child in topic.Subtopics)
                                        remaining.Enqueue (child);
                        }
                }

                private void CountDescendants ()
                {
                        totalCount = 0;
                        Queue<Topic> remaining = new Queue<Topic> ();
                        remaining.Enqueue (this);
                        while (remaining.Count > 0) {
                                Topic topic = remaining.Dequeue ();
                                foreach (Topic child in topic.Subtopics) remaining.Enqueue (child);
                                totalCount++;
                        }
                }

                private void CalculateLevel ()
                {
                        level = 0;
                        Queue<Topic> remaining = new Queue<Topic> ();

                        if (this.Parent != null) remaining.Enqueue (this.Parent);

                        while (remaining.Count > 0) {
                                Topic topic = remaining.Dequeue ();
                                if (topic.Parent != null) remaining.Enqueue (topic.Parent);
                                level++;
                        }
                }
        }
}