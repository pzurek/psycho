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
        using Psycho;

        public class MindModel : IModel
        {
                Topic centralTopic;
                Topic currentTopic;
                XmlElement currentXmlTopic;
                XmlElement currentXmlParent;
                XmlElement currentXmlSibling;
                XmlElement foundXmlTopic;
                XmlDocument xmlModel = new XmlDocument ();

                public MindModel ()
                {
                        CentralTopic = new Topic ();
                        SetCurrent (CentralTopic);
                        centralTopic.Text = "Central Topic";

                        XmlDeclaration declarationNode = XMLModel.CreateXmlDeclaration ("1.0", "UTF-8", "");
                        xmlModel.AppendChild (declarationNode);
                        XmlElement rootNode = xmlModel.CreateElement ("Topic");
                        rootNode.SetAttribute ("guid", CentralTopic.GUID);
                        XmlElement rootTitle = xmlModel.CreateElement ("Title");
                        rootTitle.SetAttribute ("text", CentralTopic.Text);
                        rootNode.AppendChild (rootTitle);
                        xmlModel.AppendChild (rootNode);
                        currentXmlTopic = rootNode;

                        NotifyObservers ();
                }

                List<IView> observerList = new List<IView> ();
                Topics newTopics = new Topics ();
                Topics deletedTopics = new Topics ();
                string deletedTopicPath = ("");
                Topics changedTopics = new Topics ();
                bool editPending;
                public int levelCounter;

                public Topic FindByGUID (string iGuid)
                {
                        Topic found = new Topic (0);

                        Queue<Topic> remaining = new Queue<Topic> ();
                        remaining.Enqueue (this.CentralTopic);

                        while (remaining.Count > 0) {
                                Topic topic = remaining.Dequeue ();
                                if (topic.GUID != iGuid) {
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

                public XmlElement FindXmlByGuid (string iGuid)
                {
                        string xPath = ("//Topic[@guid='" + iGuid + "']");
                        foundXmlTopic = (XmlElement) xmlModel.SelectSingleNode (xPath);
                        return foundXmlTopic;
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
                        get { return editPending; }
                        set { editPending = value; }
                }

                public void AppendSomeNodes (Topic iTopic)
                {
                        while (iTopic.Subtopics.Count < 4) {
                                Topic newTopic = new Topic (this.centralTopic.TotalCount);
                                newTopic.Parent = iTopic;
                                CreateXMLSubtopic (iTopic.GUID, newTopic.GUID, newTopic.Text);
                                iTopic.AddSubtopic (newTopic);
                                if (newTopic.Level < 5)
                                        AppendSomeNodes (newTopic);
                        }
                }

                public void CreateTopic ()
                {
                        if (CurrentTopic.Parent != null) {
                                int currentIndex = CurrentTopic.Parent.Subtopics.IndexOf (CurrentTopic);
                                Topic newTopic = new Topic (centralTopic.TotalCount);
                                newTopic.Parent = CurrentTopic.Parent;
                                CurrentTopic.Parent.AddSubtopic ((currentIndex + 1), newTopic);
                                CreateXMLTopic (CurrentTopic, newTopic);
                                SetCurrent (newTopic);
                                SetCurrentXml (CurrentTopic.GUID);
                                newTopics.Add (newTopic);
                                NotifyObservers ();
                        }
                }

                public void CreateSubtopic ()
                {
                        Topic newTopic = new Topic (centralTopic.TotalCount);
                        newTopic.Parent = CurrentTopic;
                        CurrentTopic.AddSubtopic (newTopic);
                        CreateXMLSubtopic (newTopic);
                        SetCurrent (newTopic);
                        SetCurrentXml (CurrentTopic.GUID);
                        newTopics.Add (newTopic);
                        NotifyObservers ();
                }

                public void CreateXMLSubtopic (string parentGuid, string iGuid, string iTitle)
                {
                        XmlElement newXmlTopic = xmlModel.CreateElement ("Topic");
                        newXmlTopic.SetAttribute ("guid", iGuid);
                        XmlElement newXmlTitle = xmlModel.CreateElement ("Title");
                        newXmlTitle.SetAttribute ("text", iTitle);
                        newXmlTopic.AppendChild (newXmlTitle);
                        currentXmlParent = FindXmlByGuid (parentGuid);
                        currentXmlParent.AppendChild (newXmlTopic);
                }

                public void CreateXMLTopic (string parentGuid, string prevSiblingGuid, string iGuid, string iTitle)
                {
                        XmlElement newXmlTopic = xmlModel.CreateElement ("Topic");
                        newXmlTopic.SetAttribute ("guid", iGuid);
                        XmlElement newXmlTitle = xmlModel.CreateElement ("Title");
                        newXmlTitle.SetAttribute ("text", iTitle);
                        newXmlTopic.AppendChild (newXmlTitle);
                        currentXmlParent = FindXmlByGuid (parentGuid);
                        currentXmlSibling = FindXmlByGuid (prevSiblingGuid);
                        currentXmlParent.InsertAfter (newXmlTopic, currentXmlSibling);
                }

                public void CreateXMLSubtopic (Topic iTopic)
                {
                        CreateXMLSubtopic (iTopic.Parent.GUID, iTopic.GUID, iTopic.Text);
                }

                public void CreateXMLTopic (Topic iSibling, Topic iTopic)
                {
                        CreateXMLTopic (iSibling.Parent.GUID, iSibling.GUID, iTopic.GUID, iTopic.Text);
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

                        currentXmlParent = FindXmlByGuid (CurrentTopic.Parent.GUID);
                        currentXmlTopic = FindXmlByGuid (CurrentTopic.GUID);
                        currentXmlParent.RemoveChild (currentXmlTopic);

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
                                SetCurrentXml (CurrentTopic.GUID);
                        }
                        NotifyObservers ();
                }

                public void SetTitle (string iTitle)
                {
                        CurrentTopic.Text = (iTitle);
                        changedTopics.Add (CurrentTopic);
                        NotifyObservers ();
                }

                public void AddObserver (IView iView)
                {
                        observerList.Add (iView);
                }

                public void RemoveObserver (IView iView)
                {
                        observerList.Remove (iView);
                }

                public void NotifyObservers ()
                {
                        foreach (IView view in observerList) {
                                view.Update (this);
                        }
                        ClearChanges ();
                }

                void ClearChanges ()
                {
                        newTopics.Clear ();
                        deletedTopicPath = ("");
                        deletedTopics.Clear ();
                        changedTopics.Clear ();
                }

                public void SetCurrent (string iGuid)
                {
                        CurrentTopic.IsCurrent = false;
                        Topic saughtTopic = FindByGUID (iGuid);
                        CurrentTopic = saughtTopic;
                        CurrentTopic.IsCurrent = true;
                        SetCurrentXml (CurrentTopic.GUID);
                        NotifyObservers ();
                }

                public void SetCurrent (Topic iTopic)
                {
                        if (CurrentTopic != null)
                                CurrentTopic.IsCurrent = false;
                        CurrentTopic = iTopic;
                        CurrentTopic.IsCurrent = true;
                        SetCurrentXml (CurrentTopic.GUID);
                        NotifyObservers ();
                }

                public void SetCurrentXml (string iGuid)
                {
                        string xPath = ("//Topic[@guid='" + iGuid + "']");
                        currentXmlTopic = (XmlElement) xmlModel.SelectSingleNode (xPath);
                        NotifyObservers ();
                }

                public void ExpandTopic (string iGuid, bool isExpanded)
                {
                        Topic ExpandedTopic = FindByGUID (iGuid);
                        ExpandedTopic.IsExpanded = (isExpanded);
                }

                public void ChangeTopic (Topic iTopic)
                {
                        CurrentTopic = iTopic;
                        ChangedTopics.Add (CurrentTopic);
                        NotifyObservers ();
                }

                public XmlDocument XMLModel
                {
                        get { return xmlModel; }
                }
        }
}