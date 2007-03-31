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
                        this.Text = "Topic ";
                        this.Style = new TopicStyle ();
                }

                public Topic (string iTitle)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = iTitle;
                        this.Style = new TopicStyle ();
                }

                public Topic (Topic iParent)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = "Topic ";
                        this.Parent = iParent;
                        this.Style = new TopicStyle ();
                }

                public Topic (string iTitle, Topic iParent)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = iTitle;
                        this.Parent = iParent;
                        this.Style = new TopicStyle ();
                }

                public Topic (int topicNumber)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = "Topic " + topicNumber.ToString ();
                        this.Style = new TopicStyle ();
                }

                string text;
                string number;
                Topic parent;
                Topic previous;
                Topic next;
                Topic firstAncestor;
                int level;
                int index;
                int totalCount;
                string path;
                string guid;
                bool isCurrent;
                bool isExpanded;
                bool isVisible;
                bool hasNotes;
                TopicNotes notes;
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
                                this.textLayout = new Pango.Layout (this.PangoContext);
                                this.textLayout.SetText (this.text);
                                this.textLayout.Width = Pango.Units.FromPixels (this.Style.Width);
                                this.textLayout.FontDescription = Pango.FontDescription.FromString (this.Style.StyleFont.Description);
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
                                if (this.IsExpanded && this.Subtopics.Height > this.Frame.Height)
                                        totalHeight = Subtopics.Height;
                                else
                                        totalHeight = this.Frame.Height;
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

                public Topic FirstAncestor
                {
                        get
                        {
                                Queue<Topic> remaining = new Queue<Topic> ();

                                if (this.Parent != null && !this.Parent.IsCentral)
                                        remaining.Enqueue (this.Parent);

                                while (remaining.Count > 0) {
                                        Topic topic = remaining.Dequeue ();
                                        if (topic.Level == 1)
                                                firstAncestor = topic;
                                        else
                                                remaining.Enqueue (topic.Parent);
                                }
                                return firstAncestor;
                        }
                }

                public Topic Next
                {
                        get
                        {
                                if (this.Parent != null && !this.IsLast)
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

                public TopicNotes Notes
                {
                        get
                        {
                                if (this.notes == null)
                                        notes = new TopicNotes (this);
                                return notes;
                        }
                        set
                        {
                                if (this.notes == null)
                                        notes = new TopicNotes (this);
                                notes = value;
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
                                Queue<Topic> remaining = new Queue<Topic> ();

                                if (this.Parent != null && this.Parent.IsCentral && this.Parent.IsExpanded)
                                        isVisible = true;
                                else
                                        if (this.Parent != null)
                                                remaining.Enqueue (this.Parent);

                                while (remaining.Count > 0) {
                                        Topic parent = remaining.Dequeue ();
                                        if (!parent.IsExpanded || !parent.IsVisible) {
                                                isVisible = false;
                                        }
                                        else
                                                isVisible = true;
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
                                if (this.Notes != null && this.Notes.Text != "")
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

                public void AddSubtopic (Topic iTopic)
                {
                        this.Subtopics.Add (iTopic);
                }

                public void AddSubtopic (int iIndex, Topic iTopic)
                {
                        this.Subtopics.Insert (iIndex, iTopic);
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