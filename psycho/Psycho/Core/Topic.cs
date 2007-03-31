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
	using Psycho;
	
	public partial class Topic
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Title"></param>
        public Topic(int topic_number)
        {
        	System.Guid newGuid = System.Guid.NewGuid();
        	this.guid = newGuid.ToString();
        	this.Title = ("Topic " + topic_number.ToString());
        }

        #region private fields
        private string title;
		private string id;
		private Topic parent;
		private int level;
        private int totalCount;
        private string guid;
        #endregion

        #region public fields

        public TopicCollection Subtopics = new TopicCollection();

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
		
		public int Level
		{
			get { return level; }
			set { level = value; }
		}

        public string GUID
        {
            get { return guid; }
        }
        
        public int TotalCount
        {
            get { this.CountDescendants(); return totalCount; }
        }

        #endregion

        #region public metods

        public void AddSubtopic(Topic paramTopic)
        {
            this.Subtopics.Add(paramTopic);
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
        
        public void ForEach(Action<Topic> action)
        {
            Queue<Topic> remaining = new Queue<Topic>();
            remaining.Enqueue(this);

            while (remaining.Count > 0)
            {
                Topic topic = remaining.Dequeue();
                action(topic);
                foreach (Topic child in topic.Subtopics)
                remaining.Enqueue(child);
            }
        }
        #endregion
    }
}
