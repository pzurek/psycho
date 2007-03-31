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
    using System.Xml;
    using Psycho;

    public class MindModel : IModel {

        private Topic centralTopic = new Topic (1234);
        private Topic currentTopic;
        private XmlElement currentXmlTopic;
        private XmlDocument xmlModel = new XmlDocument();

        public MindModel ()
        {
            this.currentTopic = this.centralTopic;
            centralTopic.Title = "Central Topic";

            XmlDeclaration declarationNode = XMLModel.CreateXmlDeclaration ("1.0", "UTF-8", "");
            xmlModel.AppendChild (declarationNode);
            XmlElement rootNode = xmlModel.CreateElement ("Topic");
            rootNode.SetAttribute ("guid", CentralTopic.GUID);
            XmlElement rootTitle = xmlModel.CreateElement ("Title");
            rootTitle.SetAttribute ("text", CentralTopic.Title);
            rootNode.AppendChild (rootTitle);
            xmlModel.AppendChild (rootNode);
            currentXmlTopic = rootNode;

            NotifyObservers ();
        }

        #region IModel Members
        private ArrayList observerList = new ArrayList ();
        private Topics newTopics = new Topics ();
        private Topics deletedTopics = new Topics ();
        private string deletedTopicPath = ("");
        private Topics changedTopics = new Topics ();
        private bool editPending;
        public int levelCounter;

        public Topic FindByGUID (string paramGuid/*, Topic paramTopic*/)
        {
            Topic found = new Topic (0);

            Queue<Topic> remaining = new Queue<Topic> ();
            remaining.Enqueue (this.CentralTopic);

            while (remaining.Count > 0) {
                Topic topic = remaining.Dequeue ();
                if (topic.GUID != paramGuid) {
                    foreach (Topic child in topic.Subtopics) {
                        remaining.Enqueue (child);
                    }
                }
                else {
                    found = topic;
                    break;
                }
            }
            return found;
        }

        public Topic CurrentTopic
        {
            get { return currentTopic; }
            set { currentTopic = value; }
        }

        public Topics NewTopics
        {
            get { return newTopics; }
        }

        public Topics ChangedTopics
        {
            get { return changedTopics; }
        }

        public string DeletedTopicPath
        {
            get { return deletedTopicPath; }
        }

        public Topics DeletedTopics
        {
            get { return deletedTopics; }
        }

        public Topic CentralTopic
        {
            get { return centralTopic; }
            set { centralTopic = value; }
        }

        public int CurrentLevel
        {
            get { return currentTopic.Level; }
        }

        public bool EditPending
        {
            get { Console.WriteLine ("Edit pending {0}", editPending.ToString ()); return editPending; }
            set { editPending = value; Console.WriteLine ("Edit pending {0}", editPending.ToString ()); }
        }

        public void AppendSomeNodes (Topic paramTopic)
        {
            while (paramTopic.Subtopics.Count < 2) {
                Topic newTopic = new Topic (this.centralTopic.TotalCount);
                newTopic.Parent = paramTopic;
                CreateXMLTopic (paramTopic.GUID, newTopic.GUID, newTopic.Title);
                paramTopic.AddSubtopic (newTopic);
                if (newTopic.Level < 4)
                    AppendSomeNodes (newTopic);
            }
        }

        public void CreateTopic ()
        {
            if (CurrentTopic.Parent != null) {
                int currentIndex = CurrentTopic.Parent.Subtopics.IndexOf (CurrentTopic);
                Topic newTopic = new Topic (centralTopic.TotalCount);
                newTopic.Parent = CurrentTopic.Parent;
                CurrentTopic.Parent.AddSubtopicAt ((currentIndex + 1), newTopic);
                CreateXMLTopic (CurrentTopic.Parent.GUID, newTopic.GUID, newTopic.Title);
                CurrentTopic = newTopic;
                newTopics.Add (newTopic);
                NotifyObservers ();
            }
        }

        public void CreateXMLTopic (string parentGuid, string paramGuid, string paramTitle)
        {
            XmlElement newXmlTopic = xmlModel.CreateElement ("Topic");
            newXmlTopic.SetAttribute ("guid", paramGuid);
            XmlElement newXmlTitle = xmlModel.CreateElement ("Title");
            newXmlTitle.SetAttribute ("text", paramTitle);
            newXmlTopic.AppendChild (newXmlTitle);
            currentXmlTopic.AppendChild (newXmlTopic);
        }

        public void CreateSubtopic ()
        {
            Topic newTopic = new Topic (centralTopic.TotalCount);
            newTopic.Parent = CurrentTopic;
            CurrentTopic.AddSubtopic (newTopic);
            CreateXMLTopic (CurrentTopic.GUID, newTopic.GUID, newTopic.Title);
            CurrentTopic = newTopic;
            newTopics.Add (newTopic);
            NotifyObservers ();
        }

        public void DeleteTopic ()
        {
            int newIndex;
            int currentIndex;

            if (CurrentTopic.Parent != null) {
                currentIndex = CurrentTopic.Parent.Subtopics.IndexOf (CurrentTopic);
            }
            else
                return;

            Topic tempParent = this.CurrentTopic.Parent;
            Topic deletedTopic = (CurrentTopic);

            deletedTopicPath = (deletedTopic.Path);
            deletedTopics.Add (deletedTopic);

            if (CurrentTopic.Parent.Subtopics.Count == 1) {
                CurrentTopic.Parent.Subtopics.Clear ();
                CurrentTopic = tempParent;
            }
            else {
                if (currentIndex == (CurrentTopic.Parent.Subtopics.Count - 1)) {
                    newIndex = currentIndex - 1;
                }
                else {
                    newIndex = currentIndex;
                }
                CurrentTopic.Parent.Subtopics.RemoveAt (currentIndex);
                CurrentTopic = tempParent.Subtopics[newIndex];
            }
            NotifyObservers ();
        }

        public void SetTitle (string paramTitle)
        {
            CurrentTopic.Title = (paramTitle);
            changedTopics.Add (CurrentTopic);
            NotifyObservers ();
        }

        public void AddObserver (IView paramView)
        {
            observerList.Add (paramView);
        }

        public void RemoveObserver (IView paramView)
        {
            observerList.Remove (paramView);
        }

        public void NotifyObservers ()
        {
            foreach (IView view in observerList) {
                view.Update (this);
            }
            ClearChanges ();
        }

        private void ClearChanges ()
        {
            newTopics.Clear ();
            deletedTopicPath = ("");
            deletedTopics.Clear ();
            changedTopics.Clear ();
        }

        public void SetCurrent (string paramGuid, Topic paramTopic)
        {
            Topic saughtTopic = FindByGUID (paramGuid);
            CurrentTopic = saughtTopic;
            currentXmlTopic = xmlModel.GetElementById (paramGuid);
            NotifyObservers ();
        }

        public void ExpandTopic (string paramGuid, bool isExpanded)
        {
            Topic ExpandedTopic = FindByGUID (paramGuid);
            ExpandedTopic.IsExpanded = (isExpanded);
        }
        #endregion

        /*

        public XmlNode declarationNode;
        public XmlNode rootNode;
        public XmlElement topicNode;
        public XmlAttribute nodeGUID;
        public XmlElement topicTitle;
        public XmlAttribute titleText;
        public XmlNodeList nodeSubtopics;

        public void BuildXML (Topic paramTopic)
        {
            if (xmlModel != null) {

                foreach (Topic subtopic in paramTopic.Subtopics) {
                    topicNode = xmlModel.CreateElement ("Topic");
                    topicNode.SetAttribute ("guid", subtopic.GUID);
                    topicTitle = xmlModel.CreateElement ("Title");
                    topicTitle.SetAttribute ("text", subtopic.Title);
                    topicNode.AppendChild (topicTitle);
                    BuildXML (subtopic);
                }
            }
        }
         
        */

        public void ChangeTopic (Topic paramTopic)
        {
            CurrentTopic = paramTopic;
            ChangedTopics.Add (CurrentTopic);
            NotifyObservers ();
        }

        public XmlDocument XMLModel
        {
            get { return xmlModel; }
        }
    }
}