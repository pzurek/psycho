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
	
    /// <summary>
    /// Implementation of the IPsychoModel interface for the Topic object.
    /// </summary>
    public partial class MindModel : IPsychoModel
    {
    	private Topic centralTopic = new Topic(0);
    	private Topic currentTopic;
        private int currentLevel = 0;
    	
        #region IPsychoModel Members
        private ArrayList observerList = new ArrayList();
         
        public Topic CurrentTopic
        {
			get	{ return currentTopic; }
			set { currentTopic = value; }
        }

        public Topic CentralTopic
        {
            get { return centralTopic; }
            set { centralTopic = value; }
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }
        
        public void CreateTopic()
        {
            Topic newTopic = new Topic(centralTopic.TotalCount);
            newTopic.Parent = CurrentTopic.Parent;
            CurrentTopic.Parent.AddSubtopic(newTopic);
            CurrentTopic = newTopic;
            NotifyObservers();
        }
        
        public void CreateSubtopic()
        {
            Topic newTopic = new Topic(centralTopic.TotalCount);
            newTopic.Parent = CurrentTopic;
            CurrentTopic.AddSubtopic(newTopic);
            CurrentTopic = newTopic;
            NotifyObservers();
        }

        public void DeleteTopic()
        {
            int previousIndex;
            int currentIndex = CurrentTopic.Parent.Subtopics.IndexOf(CurrentTopic);

            if (currentIndex > 0) {
                previousIndex = currentIndex - 1;
            }
            else {
                previousIndex = 0;
            }

            Topic tempParent = this.CurrentTopic.Parent;
            CurrentTopic.Parent.Subtopics.RemoveAt(currentIndex);
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
            foreach (IPsychoView view in observerList) {
                view.Update(this);
            }
        }
        
        public void SetCurrent(string paramGuid, Topic paramTopic)
        {

            foreach (Topic topic in paramTopic.Subtopics) {
                if (topic.GUID == paramGuid) {
                    CurrentTopic = topic;
                    break;
                }
                else SetCurrent(paramGuid, topic);
            }
            //if (paramTopic.GUID == paramGuid) {
            //        CurrentTopic = paramTopic;
            //    }
            //else {
            //    foreach (Topic child in paramTopic.Subtopics) {
            //        if (child.GUID == paramGuid) {
            //            CurrentTopic = child;
            //            break;
            //        }
            //        SetCurrent (paramGuid, child);
            //    }
            //}
            Console.WriteLine("Current topic set to: " + CurrentTopic.GUID);
        }
        #endregion
    }
}
