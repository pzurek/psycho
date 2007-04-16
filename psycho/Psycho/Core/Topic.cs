//------10--------20--------30--------40--------50--------60--------70--------80
//
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
        using System.Xml;
        using System.Diagnostics;
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
                        this.Style = new TopicStyle (this);
                        if (this.Parent != null)
                                this.Parent.Update ();
                }

                public Topic (string iTitle)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = iTitle;
                        this.Style = new TopicStyle (this);
                        if (this.Parent != null)
                                this.Parent.Update ();
                }

                public Topic (Topic iParent)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = "Topic ";
                        this.Parent = iParent;
                        iParent.SubtopicList.Add (this);
                        this.Style = new TopicStyle (this);
                        if (this.Parent != null)
                                this.Parent.Update ();
                }

                public Topic (string iTitle, Topic iParent)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = iTitle;
                        this.Parent = iParent;
                        iParent.SubtopicList.Add (this);
                        this.Style = new TopicStyle (this);
                        if (this.Parent != null)
                                this.Parent.Update ();
                }

                public Topic (int topicNumber)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = "Topic " + topicNumber.ToString ();
                        this.Style = new TopicStyle (this);
                        if (this.Parent != null)
                                this.Parent.Update ();
                }

                public Topic (int topicNumber, Topic iParent)
                {
                        System.Guid newGuid = System.Guid.NewGuid ();
                        this.guid = newGuid.ToString ();
                        this.Text = "Topic " + topicNumber.ToString ();
                        this.Parent = iParent;
                        iParent.SubtopicList.Add (this);
                        this.Style = new TopicStyle (this);
                        if (this.Parent != null)
                                this.Parent.Update ();
                }

                public void Update ()
                {
                        this.UpdateTextSize (this);
                        this.Frame.Update (this);
                }

                void UpdateTextSize (Topic iTopic)
                {
                        int w, h;
                        iTopic.TextLayout.GetPixelSize (out w, out h);
                        textWidth = w;
                        textHeight = h;
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
                bool isValid;
                bool isCurrent;
                bool isExpanded;
                bool isVisible;
                bool hasNote;
                bool containsPoint;
                bool regionContainsPoint;
                int top, bottom, left, right;
                TopicNote note;
                TopicStyle style;
                TopicOffset offset;
                Title topicTitle;
                TopicType type;
                int textWidth;
                double width;
                double totalWidth;
                int textHeight;
                double height;
                double totalHeight;
                TopicList subtopicList;
                Pango.Layout textLayout;
                TopicFrame frame;
                TopicConnection connection;
                bool inPrimarySubtopicList;
                bool isFirst;
                bool isLast;
                bool hasChildren;
                Cairo.PointD inPoint;
                Cairo.PointD outPoint;

                public TopicList SubtopicList
                {
                        get
                        {
                                if (subtopicList == null)
                                        subtopicList = new TopicList (this);
                                return subtopicList;
                        }
                }

                public int Index
                {
                        get
                        {
                                if (this.Parent != null) {
                                        index = this.Parent.SubtopicList.IndexOf (this);
                                        return index;
                                }
                                else {
                                        index = -1;
                                        return index;
                                }
                        }
                }

                public Cairo.PointD InPoint
                {
                        get
                        {
                                switch (this.Parent.Style.SubLayout) {
                                        case SubtopicLayout.Map: {
                                                if (this.InPrimarySubtopicList)
                                                        inPoint = this.Frame.Left;
                                                else
                                                        inPoint = this.Frame.Right;
                                        }
                                        break;
                                        case SubtopicLayout.Tree: {
                                                if (this.InPrimarySubtopicList)
                                                         inPoint = this.Frame.Left;
                                                else
                                                         inPoint = this.Frame.Right;
                                        }
                                        break;
                                        case SubtopicLayout.OrgChart:
                                        inPoint = this.Frame.Top;
                                        break;
                                }
                                return inPoint;
                        }
                }


                public Cairo.PointD OutPoint
                {
                        get
                        {
                                if (this.Style.ConnectPoint == ConnectionPoint.Center)
                                        outPoint = this.Frame.Center;
                                else
                                        switch (this.Style.SubLayout) {
                                                case SubtopicLayout.Map: {
                                                        if (this.InPrimarySubtopicList)
                                                                outPoint = this.Frame.Right;
                                                        else
                                                                outPoint = this.Frame.Left;
                                                }
                                                break;
                                                case SubtopicLayout.Tree:
                                                outPoint = this.Frame.Bottom;
                                                break;
                                                case SubtopicLayout.OrgChart:
                                                outPoint = this.Frame.Bottom;
                                                break;
                                        }
                                return outPoint;
                        }
                }

                public int Top
                {
                        get
                        {
                                return top;
                        }
                }

                public int Bottom
                {
                        get
                        {
                                return bottom;
                        }
                }

                public int Left
                {
                        get
                        {
                                return left;
                        }
                }

                public int Right
                {
                        get
                        {
                                return right;
                        }
                }

                public int GlobalWidth
                {
                        get
                        {
                                int globalWidth = right - left;
                                return globalWidth;
                        }
                }

                public int GlobalHeight
                {
                        get
                        {
                                int globalHeight = bottom - top;
                                return globalHeight;
                        }
                }

                public void UpdateLimits ()
                {
                        updateTop ();
                        updateBottom ();
                        updateLeft ();
                        updateRight ();
                }

                void updateTop ()
                {
                        this.top = (int) this.Frame.Top.Y;
                        foreach (Topic topic in this.SubtopicList) {
                                if (topic.Top < this.Top)
                                        this.top = topic.Top;
                        }
                }

                void updateBottom ()
                {
                        this.bottom = (int) this.Frame.Bottom.Y;
                        foreach (Topic topic in this.SubtopicList) {
                                if (topic.bottom > this.Bottom)
                                        this.bottom = topic.Bottom;
                        }
                }

                void updateLeft ()
                {
                        this.left = (int) this.Frame.Left.X;
                        foreach (Topic topic in this.SubtopicList) {
                                if (topic.left < this.Left)
                                        this.left = topic.Left;
                        }
                }

                void updateRight ()
                {
                        this.right = (int) this.Frame.Right.X;
                        foreach (Topic topic in this.SubtopicList) {
                                if (topic.right > this.Right)
                                        this.right = topic.Right;
                        }
                }

                public bool RegionContainsPoint (int iX, int iY)
                {
                        regionContainsPoint = false;
                        if ((iY > this.Top && iY < this.Bottom) && (iX > this.Left && iX < this.Right))
                                regionContainsPoint = true;
                        return regionContainsPoint;
                }

                public bool ContainsPoint (int iX, int iY)
                {
                        containsPoint = false;
                        if ((iY > this.Frame.Top.Y && iY < this.Frame.Bottom.Y)
                                && (iX > this.Frame.Left.X && iX < this.Frame.Right.X)) {
                                containsPoint = true;
                                Console.WriteLine ("Top: " + this.Frame.Top.Y +
                                                   "\nBottom: " + this.Frame.Bottom.Y +
                                                   "\nLeft: " + this.Frame.Left.X +
                                                   "\nRight: " + this.Frame.Right.X);
                        }
                        return containsPoint;
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
                                if (this.textLayout == null)
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
                                if (textWidth == 0)
                                        this.UpdateTextSize (this);
                                return textWidth;
                        }
                }

                public int TextHeight
                {
                        get
                        {
                                if (textWidth == 0)
                                        this.UpdateTextSize (this);
                                return textHeight;
                        }
                }

                public double Width
                {
                        get {
                                width = this.Frame.Width + this.Style.Padding + this.Style.StrokeWidth;
                                return width;
                        }
                }

                public double Height
                {
                        get {
                                height = this.Frame.Height + this.Style.Padding + this.Style.StrokeWidth;
                                return height;
                        }
                }

                public double TotalHeight
                {
                        get
                        {
                                if (this.IsExpanded && this.SubtopicList.Count > 0)
                                        switch (this.Style.SubLayout) {
                                                case SubtopicLayout.Map:
                                                if (this.SubtopicList.Height > this.Height)
                                                        totalHeight = this.SubtopicList.Height;
                                                break;
                                                case SubtopicLayout.Tree:
                                                totalHeight = this.Height + this.SubtopicList.Height + this.Style.VerChildDist;
                                                break;
                                                case SubtopicLayout.OrgChart:
                                                totalHeight = this.Height + this.SubtopicList.Height + this.Style.OrgChartVertDist;
                                                break;
                                        }
                                else
                                        totalHeight = this.Height;
                                return totalHeight;
                        }
                }

                public double TotalWidth
                {
                        get
                        {
                                        if (this.IsExpanded && this.SubtopicList.Count > 0)
                                                switch (this.Style.SubLayout) {
                                                        case SubtopicLayout.Map:
                                                        totalWidth = this.Width + this.SubtopicList.Width + this.Style.HorChildDist;
                                                        break;
                                                        case SubtopicLayout.Tree:
                                                        totalWidth = this.Width / 2  + this.SubtopicList.Width + this.Style.HorChildDist / 2;
                                                        break;
                                                        case SubtopicLayout.OrgChart:
                                                        if (this.SubtopicList.Width > this.Width)
                                                                totalWidth = this.SubtopicList.Width;
                                                        break;
                                                }
                                        else
                                        totalWidth = this.Width;
                                return totalWidth;
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

                public TopicConnection Connection
                {
                        get
                        {
                                if (this.connection == null)
                                        connection = new TopicConnection (this);
                                return connection;
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
                                        next = this.Parent.SubtopicList[(this.Index + 1)];
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
                                        previous = this.Parent.SubtopicList[(this.Index - 1)];
                                else
                                        previous = null;
                                return previous;
                        }
                }

                public string GUID
                {
                        get { return guid; }
                }

                public TopicNote Note
                {
                        get
                        {
                                if (this.note == null)
                                        note = new TopicNote (this);
                                return note;
                        }
                        set
                        {
                                if (this.note == null)
                                        note = new TopicNote (this);
                                note = value;
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

                public bool InPrimarySubtopicList
                {
                        get { return inPrimarySubtopicList; }
                        set { inPrimarySubtopicList = value; }
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
                                if (this.Parent != null && this.Parent.SubtopicList.Count == this.Index + 1)
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
                                if (SubtopicList.Count > 0)
                                        hasChildren = true;
                                else
                                        hasChildren = false;
                                return hasChildren;
                        }
                }

                public bool HasNote
                {
                        get
                        {
                                if (this.Note != null)
                                        hasNote = !String.IsNullOrEmpty (this.Note.Text);
                                return hasNote;
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

                public int TotalCount
                {
                        get
                        {
                                totalCount = 0;
                                Queue<Topic> remaining = new Queue<Topic> ();
                                remaining.Enqueue (this);
                                while (remaining.Count > 0) {
                                        Topic topic = remaining.Dequeue ();
                                        foreach (Topic child in topic.SubtopicList) remaining.Enqueue (child);
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
                                        this.path = (this.Parent.Path + ":" + this.Parent.SubtopicList.IndexOf (this).ToString ());
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
                                                this.number = ((this.Parent.SubtopicList.IndexOf (this) + 1).ToString ());
                                        else
                                                this.number = (this.Parent.Number.ToString () + "." + (this.Parent.SubtopicList.IndexOf (this) + 1).ToString ());
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

                public Topic FirstChild ()
                {
                        return this.SubtopicList.First;
                }

                public Topic LastChild ()
                {
                        return this.SubtopicList.Last;
                }

                public void AddSubtopic (Topic iTopic)
                {
                        this.SubtopicList.Add (iTopic);
                }

                public void AddSubtopic (int iIndex, Topic iTopic)
                {
                        this.SubtopicList.Insert (iIndex, iTopic);
                }

                public void Delete ()
                {
                        if (this.Parent != null)
                                this.Parent.SubtopicList.Remove (this);
                }

                public void ForEach (Action<Topic> action)
                {
                        Queue<Topic> remaining = new Queue<Topic> ();

                        remaining.Enqueue (this);

                        while (remaining.Count > 0) {
                                Topic topic = remaining.Dequeue ();
                                action (topic);
                                foreach (Topic child in topic.SubtopicList)
                                        remaining.Enqueue (child);
                        }
                }

                public bool IsValid
                {
                        get { return isValid; }
                }

                public void Invalidate ()
                {
                        isValid = false;
                }

                public void Validate ()
                {
                        isValid = true;
                }
        }
}