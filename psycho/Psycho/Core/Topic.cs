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
                        this.IsExpanded = false;
                        this.Style = (new TopicStyle ());
                        this.TextLayout.FontDescription = Pango.FontDescription.FromString (this.Style.StyleFont.Description);
                        this.TextLayout.SetText (this.Text);
                }

                public Topic (string iTitle)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = iTitle;
                        this.IsExpanded = false;
                        this.Style = (new TopicStyle ());
                        this.TextLayout.FontDescription = Pango.FontDescription.FromString (this.Style.StyleFont.Description);
                        this.TextLayout.SetText (this.Text);
                }

                public Topic (Topic paramParent)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = ("Topic ");
                        this.Parent = paramParent;
                        this.IsExpanded = false;
                        this.Style = (new TopicStyle ());
                        this.TextLayout.FontDescription = Pango.FontDescription.FromString (this.style.StyleFont.Description);
                        this.TextLayout.SetText (this.text);
                }

                public Topic (int topicNumber)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = ("Topic " + topicNumber.ToString ());
                        this.IsExpanded = false;
                        this.Style = (new TopicStyle ());
                        this.TextLayout.FontDescription = Pango.FontDescription.FromString (this.style.StyleFont.Description);
                        this.TextLayout.SetText (this.text);
                }

                string text;
                string number;
                Topic parent;
                Topic previous;
                Topic next;
                int level;
                int index;
                int totalCount;
                string path;
                string guid;
                bool isCurrent;
                bool isExpanded;
                bool isVisible;
                bool hasNotes;
                Notes topicNotes;
                TopicStyle style;
                TopicOffset offset;
                Title topicTitle;
                TopicType type;
                int textWidth;
                int totalWidth;
                int textHeight;
                int totalHeight;
                Topics subtopics;
                Pango.Layout textLayout;
                TopicFrame frame;
                bool isOnLeft;
                bool isFirst;
                bool isLast;
                bool hasChildren;

                public Topics Subtopics
                {
                        get
                        {
                                if (subtopics == null)
                                        subtopics = new Topics (this);
                                return subtopics;
                        }
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
                                textLayout = new Pango.Layout (this.PangoContext);
                                textLayout.SetText (this.text);
                                textLayout.Width = Pango.Units.FromPixels (this.Style.Width);
                                textLayout.FontDescription = Pango.FontDescription.FromString (this.Style.StyleFont.Description);
                                return textLayout;
                        }
                }

                public int TextWidth
                {
                        get
                        {
                                TextLayout.GetPixelSize (out textWidth, out textHeight);
                                return textWidth;
                        }
                }



                public int TextHeight
                {
                        get
                        {
                                TextLayout.GetPixelSize (out textWidth, out textHeight);
                                return textHeight;
                        }
                }

                public int TotalHeight
                {
                        get
                        {
                                totalHeight = this.Frame.Height;
                                if (this.Subtopics.Count > 0
                                        && this.IsExpanded
                                        && this.Subtopics.Height > this.totalHeight)
                                        totalHeight = Subtopics.Height;
                                return totalHeight;
                        }
                }

                public TopicFrame Frame
                {
                        get
                        {
                                if (this.frame == null)
                                        frame = new TopicFrame (this);
                                return frame;
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
                                if (this.Parent != null && this.Parent.Subtopics.Count > this.Index)
                                        next = this.Parent.Subtopics[(this.Index + 1)];
                                else
                                        next = null;
                                return next;
                        }

                }

                public Topic Previous
                {
                        get
                        {
                                if (this.Index > 0)
                                        previous = this.Parent.Subtopics[(this.Index - 1)];
                                else
                                        previous = null;
                                return previous;
                        }
                }

                public string GUID
                {
                        get { return guid; }
                }

                public Notes TopicNotes
                {
                        get
                        {
                                if (this.topicNotes == null)
                                        topicNotes = new Notes (this);
                                return topicNotes;
                        }
                        set
                        {
                                if (this.topicNotes == null)
                                        topicNotes = new Notes (this);
                                topicNotes = value;
                        }
                }

                public bool IsCurrent
                {
                        get { return isCurrent; }
                        set { isCurrent = value; }
                }

                public bool IsExpanded
                {
                        get { return isExpanded; }
                        set { isExpanded = value; }
                }

                public bool IsVisible
                {
                        get
                        {
                                isVisible = true;
                                Queue<Topic> remaining = new Queue<Topic> ();

                                if (this.Parent != null) remaining.Enqueue (this.Parent);

                                while (remaining.Count > 0) {
                                        Topic topic = remaining.Dequeue ();
                                        if (topic.IsExpanded && topic.Parent.IsVisible) {
                                                isVisible = true;
                                                if (topic.IsCentral) break;
                                        }
                                        else
                                                isVisible = false;
                                }
                                return isVisible;
                        }
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

                public bool IsMain
                {
                        get
                        {
                                if (this.Level == 1)
                                        return true;
                                else
                                        return false;
                        }
                }

                public bool IsFirst
                {
                        get
                        {
                                if (this.Parent != null && this.Index == 0)
                                        isFirst = true;
                                else
                                        isFirst = false;
                                return isFirst;
                        }
                }

                public bool IsLast
                {
                        get
                        {
                                if (this.Parent != null && this.Parent.Subtopics.Count == this.Index + 1)
                                        isLast = true;
                                else
                                        isLast = false;
                                return isLast;
                        }
                }

                public bool HasChildren
                {
                        get
                        {
                                if (Subtopics.Count > 0)
                                        hasChildren = true;
                                else
                                        hasChildren = false;
                                return hasChildren;
                        }
                }

                new public TopicStyle Style
                {
                        get { return style; }
                        set { style = value; }
                }

                public TopicOffset Offset
                {
                        get
                        {
                                if (this.offset == null)
                                        offset = new TopicOffset (this);
                                return offset;
                        }
                }

                public bool HasNotes
                {
                        get
                        {
                                if (this.TopicNotes != null && this.TopicNotes.Text != "")
                                        hasNotes = true;
                                else
                                        hasNotes = false;
                                return hasNotes;
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
                                        this.path = (this.Parent.Path + ":" + this.Parent.Subtopics.IndexOf (this).ToString ());
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