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

    /// <summary>
    /// Implementation of the IPsychoModel interface for the Topic object.
    /// </summary>
    public partial class MindModel : IPsychoModel {

        private Topic centralTopic = new Topic(0);
        private Topic currentTopic;
        private int currentLevel = 0;

        #region IPsychoModel Members
        private ArrayList observerList = new ArrayList();

        public Topic CurrentTopic
        {
            get { return currentTopic; }
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
            int currentIndex = CurrentTopic.Parent.Subtopics.IndexOf(CurrentTopic);
            Topic newTopic = new Topic(centralTopic.TotalCount);

            if (CurrentTopic.Parent != null) {
                newTopic.Parent = CurrentTopic.Parent;
                CurrentTopic.Parent.AddSubtopicAt((currentIndex + 1), newTopic);
                CurrentTopic = newTopic;
            }
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
            int newIndex;
            int currentIndex = CurrentTopic.Parent.Subtopics.IndexOf(CurrentTopic);
            Topic tempParent = this.CurrentTopic.Parent;

            if (CurrentTopic.Parent.Subtopics.Count == 1) {
                CurrentTopic.Parent.Subtopics.Clear();
                CurrentTopic = tempParent;
            }
            else {
                if (currentIndex == (CurrentTopic.Parent.Subtopics.Count - 1)) {
                    newIndex = currentIndex - 1;
                }
                else {
                    newIndex = currentIndex;
                }
                CurrentTopic.Parent.Subtopics.RemoveAt(currentIndex);
                CurrentTopic = tempParent.Subtopics[newIndex];
            }
            NotifyObservers();
        }

        public void SetTitle(string paramTitle)
        {
            CurrentTopic.Title = (paramTitle);
            NotifyObservers();
        }

        public void AddObserver(IPsychoView paramView)
        {
            observerList.Add(paramView);
            Console.WriteLine("View: " + paramView.ToString() + " added to observer list");
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
            Topic saughtTopic = FindByGUID(paramGuid);
            //Console.WriteLine("Found topic: " + saughtTopic.Title);
            CurrentTopic = saughtTopic;
            //Console.WriteLine("Current topic set to: " + CurrentTopic.Title);
            NotifyObservers();
        }

        public void ExpandTopic(string paramGuid, bool isExpanded)
        {
            Topic ExpandedTopic = FindByGUID(paramGuid);
            ExpandedTopic.IsExpanded = (isExpanded);
        }
        #endregion
    }
}
