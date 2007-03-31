namespace Psycho
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Psycho;
	
    /// <summary>
    /// Implementation of the IPsychoModel interface for the Topic object.
    /// </summary>
    public partial class MindModel : IPsychoModel
    {
    	private Topic centralTopic = new Topic();
    	private Topic currentTopic;
        private int currentLevel = 0;
    	
        #region IPsychoModel Members
        private ArrayList observerList = new ArrayList();
         
        public Topic CurrentTopic
        {
			get	{ return this.currentTopic; }
			set { this.currentTopic = value; }
        }

        public Topic CentralTopic
        {
            get { return this.centralTopic; }
            set { this.centralTopic = value; }
        }

        public int CurrentLevel
        {
            get { return this.currentLevel; }
            set { this.currentLevel = value; }
        }
        
        public void AddTopic()
        {
            Topic newTopic = new Topic();
            newTopic.Parent = CurrentTopic.Parent;
            this.CurrentTopic.Parent.Add(newTopic);
            this.CurrentTopic = newTopic;
            NotifyObservers();
        }
        
        public void AddSubtopic()
        {
            Topic newTopic = new Topic();
            newTopic.Parent = CurrentTopic;
            this.CurrentTopic.Add(newTopic);
            this.CurrentTopic = newTopic;
            NotifyObservers();
        }

        public void Delete()
        {
            int previousIndex;
            int currentIndex = this.CurrentTopic.Parent.Subtopics.IndexOf(CurrentTopic);
            if (currentIndex > 0)
            {
                previousIndex = currentIndex - 1;
            }
            else
            {
                previousIndex = 0;
            }
            Topic tempParent = this.CurrentTopic.Parent;
            this.CurrentTopic.Delete();
            CurrentTopic = tempParent.Subtopics[previousIndex];
            NotifyObservers();
        }

        public void SetTitle(string paramTitle)
        {
            this.CurrentTopic.Title = (paramTitle);
        }

        public void AddObserver(IPsychoView paramView)
        {
            observerList.Add(paramView);
        }

        public void RemoveObserver(IPsychoView paramView)
        {
            observerList.Remove(paramView);
        }

        public void NotifyObservers()
        {
            foreach (IPsychoView view in observerList)
            {
                view.Update(this);
            }
        }
        
        public void SetCurrent(string paramGuid)
        {
            Queue<Topic> remaining = new Queue<Topic>();
            remaining.Enqueue(CentralTopic);

            while (remaining.Count > 0)
            {
                Topic topic = remaining.Dequeue();
               	if (topic.GUID == paramGuid)
               		{CurrentTopic = topic; break;}
                foreach (Topic child in topic.Subtopics)
                {
                	remaining.Enqueue(child);
                	if (child.GUID == paramGuid)
                		{CurrentTopic = child; break;};
                }
            }
            Console.WriteLine("Current topic set to: " + CurrentTopic.GUID);
        }
        #endregion
    }
}
