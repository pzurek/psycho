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
    using Psycho;

    public partial class Topic {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Title"></param>
        public Topic(int topic_number)
        {
            System.Guid newGuid = System.Guid.NewGuid();
            this.guid = newGuid.ToString();
            this.Title = ("Topic " + topic_number.ToString());
            this.isExpanded = false;
        }

        #region private fields
        private string title;
        private string id;
        private Topic parent;
        private int level;
        private int totalCount;
        private string topicPath;
        private string guid;
        private bool isExpanded;
        #endregion

        #region public fields

        public Topics Subtopics = new Topics();

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public Topic Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set { isExpanded = value; }
        }

        public string GUID
        {
            get { return guid; }
        }

        public int TotalCount
        {
            get { this.CountDescendants(); return totalCount; }
        }

        public string TopicPath
        {
            get { this.BuildPath(); return topicPath; }
        }

        public int Level
        {
            get { this.CalculateLevel(); return level; }
        }


        #endregion

        #region public metods

        public void AddSubtopic(Topic paramTopic)
        {
            this.Subtopics.Add(paramTopic);
        }

        public void AddSubtopicAt (int paramIndex, Topic paramTopic)
        {
            this.Subtopics.Insert(paramIndex, paramTopic);
        }

        public void Delete()
        {
            if (this.Parent != null)
                this.Parent.Subtopics.Remove(this);
        }

        public void SetId()
        {
            this.id = (this.Parent.Id.ToString() + "." + this.Parent.Subtopics.IndexOf(this));
        }


        private void BuildPath ()
        {
            if (this.Parent == null)
                this.topicPath = "0";
            else
                this.topicPath = (this.Parent.topicPath + ":" + this.Parent.Subtopics.IndexOf(this).ToString());
        }

        public void ForEach(Action<Topic> action)
        {
            Queue<Topic> remaining = new Queue<Topic>();
            
            remaining.Enqueue(this);

            while (remaining.Count > 0) {
                Topic topic = remaining.Dequeue();
                action(topic);
                foreach (Topic child in topic.Subtopics)
                    remaining.Enqueue(child);
            }
        }

        private void CalculateLevel ()
        {
            level = 0;
            Queue<Topic> remaining = new Queue<Topic>();

            if (this.Parent != null) remaining.Enqueue(this.Parent);
            
            while (remaining.Count > 0) {
                Topic topic = remaining.Dequeue();
                if (topic.Parent != null) remaining.Enqueue(topic.Parent);
                level++;
            }
        }
        #endregion
    }
}
