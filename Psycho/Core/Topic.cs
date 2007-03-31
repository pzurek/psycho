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
        public Topic()
        {
        	System.Guid newGuid = System.Guid.NewGuid();
        	this.guid = newGuid.ToString();
        	this.Title = ("Topic");
            if (this.Parent != null)
                Console.WriteLine("Node created. Parent GUID: " + this.Parent.GUID);
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

        public List<Topic> Subtopics = new List<Topic>();

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
            get { return totalCount; }
        }

        #endregion

        #region public metods

        public void Add(Topic paramTopic)
        {
            this.Subtopics.Add(paramTopic);
        }

        public void Delete()
        {
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
