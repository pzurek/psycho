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
        using System.Collections;
        using System.Collections.Generic;
        using System.Xml;
        using Gtk;
        using Pango;
        using Psycho;

        public class Topic : Widget, ITopic
        {
                public Topic ()
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = ("Topic ");
                        this.isExpanded = false;
                        this.style = (new TopicStyle ());
                        textLayout = new Pango.Layout (this.PangoContext);
                        Pango.AttrForeground textColor = new AttrForeground (this.style.StyleFont.FontColor.ToPangoColor ());
                }

                public Topic (int topicNumber)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = ("Topic " + topicNumber.ToString ());
                        this.IsExpanded = false;
                        this.Style = (new TopicStyle ());
                        this.textLayout = new Pango.Layout (this.PangoContext);
                        this.textLayout.FontDescription = Pango.FontDescription.FromString (this.style.StyleFont.Description);
                        this.textLayout.SetText (this.text);
                }

                string text;
                string number;
                Topic parent;
                int level;
                int index;
                int totalCount;
                string path;
                string guid;
                bool isExpanded;
                Notes topicNotes;
                TopicStyle style;
                TopicOffset offset;
                Title topicTitle;
                TopicType type;
                int textWidth;
                int textHeight;
                Topics subtopics = new Topics ();
                Pango.Layout textLayout;
                bool isOnLeft;

                public Topics Subtopics
                {
                        get { return subtopics; }
                        set { subtopics = value; }
                }

                public int Index
                {
                        get
                        {
                                if (this.Parent != null) {
                                        index = this.Parent.Subtopics.IndexOf (this);
                                        return index;
                                }
                                else {
                                        index = -1;
                                        return index;
                                }
                        }
                }

                public string Text
                {
                        get { return text; }
                        set { text = value; }
                }

                public Pango.Layout TextLayout
                {
                        get
                        {
                                textLayout.SetText (this.text);
                                return textLayout;
                        }
                }

                public int TextWidth
                {
                        get
                        {
                                textLayout.GetPixelSize (out textWidth, out textHeight);
                                return textWidth;
                        }
                }

                public int TextHeight
                {
                        get
                        {
                                textLayout.GetPixelSize (out textWidth, out textHeight);
                                return textHeight;
                        }
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

                new public Topic Parent
                {
                        get { return parent; }
                        set { parent = value; }
                }

                public Topic Next
                {
                        get
                        {
                                if (this.Parent.Subtopics.Count > this.Index)
                                        return this.Parent.Subtopics[(this.index + 1)];
                                else
                                        return null;
                        }

                }

                public Topic Previous
                {
                        get
                        {
                                if (this.Index > 0)
                                        return this.Parent.Subtopics[(this.index - 1)];
                                else
                                        return null;
                        }
                }

                public string GUID
                {
                        get { return guid; }
                }

                public Notes TopicNotes
                {
                        get {
                                if (this.topicNotes == null)
                                        topicNotes = new Notes (this);
                                return topicNotes;
                        }
                        set {
                                if (this.topicNotes == null)
                                        topicNotes = new Notes (this);
                                topicNotes = value;
                        }
                }

                public bool IsExpanded
                {
                        get { return isExpanded; }
                        set { isExpanded = value; }
                }

                public bool IsOnLeft
                {
                        get { return isOnLeft; }
                        set { isOnLeft = value; }
                }

                public bool IsCentral
                {
                        get
                        {
                                if (this.Parent == null)
                                        return true;
                                else
                                        return false;
                        }
                }

                public bool IsFirst
                {
                        get
                        {
                                if (this.Parent != null || this.index == 0)
                                        return true;
                                else
                                        return false;
                        }
                }

                public bool IsLast
                {
                        get
                        {
                                if (this.Parent != null || this.Parent.Subtopics.Count == this.index + 1)
                                        return true;
                                else
                                        return false;
                        }
                }

                new public TopicStyle Style
                {
                        get { return style; }
                        set { style = value; }
                }

                public TopicOffset Offset
                {
                        get { return offset; }
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
                        get
                        {
                                totalCount = 0;
                                Queue<Topic> remaining = new Queue<Topic> ();
                                remaining.Enqueue (this);
                                while (remaining.Count > 0) {
                                        Topic topic = remaining.Dequeue ();
                                        foreach (Topic child in topic.Subtopics) remaining.Enqueue (child);
                                        totalCount++;
                                }
                                return totalCount;
                        }
                }

                new public string Path
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
                        get
                        {
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
                        get
                        {
                                level = 0;
                                Queue<Topic> remaining = new Queue<Topic> ();

                                if (this.Parent != null) remaining.Enqueue (this.Parent);

                                while (remaining.Count > 0) {
                                        Topic topic = remaining.Dequeue ();
                                        if (topic.Parent != null) remaining.Enqueue (topic.Parent);
                                        level++;
                                }
                                return level;
                        }
                }

                public void AddSubtopic (Topic paramTopic)
                {
                        this.Subtopics.Add (paramTopic);
                }

                public void AddSubtopic (int paramIndex, Topic paramTopic)
                {
                        this.Subtopics.Insert (paramIndex, paramTopic);
                }

                public void Delete ()
                {
                        if (this.Parent != null)
                                this.Parent.Subtopics.Remove (this);
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
        }
}