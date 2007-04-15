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

                TopicList primarySubtopicList;
                TopicList secondarySubtopicList;

                public MindModel ()
                {
                        primarySubtopicList = new TopicList ();
                        secondarySubtopicList = new TopicList ();

                        CentralTopic = new Topic ("Psycho - free mind mapping solution");
                        SetCurrent (CentralTopic);

                        XmlDeclaration declarationNode = XMLModel.CreateXmlDeclaration ("1.0", "UTF-8", "");
                        xmlModel.AppendChild (declarationNode);
                        XmlElement rootNode = xmlModel.CreateElement ("Topic");
                        rootNode.SetAttribute ("guid", CentralTopic.GUID);
                        XmlElement rootTitle = xmlModel.CreateElement ("Title");
                        rootTitle.SetAttribute ("text", CentralTopic.Text);
                        rootNode.AppendChild (rootTitle);
                        xmlModel.AppendChild (rootNode);
                        currentXmlTopic = rootNode;
                        //NotifyObservers ();
                }

                List<IView> observerList = new List<IView> ();
                TopicList newTopics = new TopicList ();
                TopicList deletedTopics = new TopicList ();
                string deletedTopicPath = ("");
                TopicList changedTopics = new TopicList ();
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
                                        foreach (Topic child in topic.SubtopicList) {
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

                public TopicList NewTopics
                {
                        get { return newTopics; }
                }

                public TopicList ChangedTopics
                {
                        get { return changedTopics; }
                }

                public string DeletedTopicPath
                {
                        get { return deletedTopicPath; }
                }

                public TopicList DeletedTopics
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
                        SetCurrent (iTopic);
                        while (iTopic.SubtopicList.Count < 2) {
                                CreateSubtopic ();
                                AppendSomeNodes (iTopic);
                        }
                                
                        if (iTopic.Level < 2)
                                foreach (Topic subTopic in iTopic.SubtopicList)
                                        AppendSomeNodes (subTopic);
                }

                void PlaceOnSide (Topic iTopic)
                {
                        if (iTopic.Level == 1) {
                                if (primarySubtopicList.Count <= secondarySubtopicList.Count) {
                                        primarySubtopicList.Add (iTopic);
                                        iTopic.InPrimarySubtopicList = true;
                                        Console.WriteLine ("Topic :" + iTopic.Text + " inserted on primary side");
                                }
                                else {
                                        secondarySubtopicList.Add (iTopic);
                                        iTopic.InPrimarySubtopicList = false;
                                        Console.WriteLine ("Topic :" + iTopic.Text + " inserted on secondary side");
                                }
                        }
                        else
                                iTopic.InPrimarySubtopicList = iTopic.Parent.InPrimarySubtopicList;
                }

                public void CreateTopic ()
                {
                        if (CurrentTopic.Parent != null) {
                                int currentIndex = CurrentTopic.Parent.SubtopicList.IndexOf (CurrentTopic);
                                Topic newTopic = new Topic (centralTopic.TotalCount);
                                newTopic.Parent = CurrentTopic.Parent;
                                CurrentTopic.Parent.AddSubtopic ((currentIndex + 1), newTopic);
                                CreateXMLTopic (CurrentTopic, newTopic);
                                SetCurrent (newTopic);
                                SetCurrentXml (CurrentTopic.GUID);
                                PlaceOnSide (newTopic);
                                newTopics.Add (newTopic);
                                UpdateToTop (newTopic);
                                NotifyObservers ();
                        }
                }

                public void CreateSubtopic ()
                {
                        if (CurrentTopic != null) {
                                Topic newTopic = new Topic (centralTopic.TotalCount);
                                newTopic.Parent = CurrentTopic;
                                CurrentTopic.IsExpanded = true;
                                CurrentTopic.AddSubtopic (newTopic);
                                CreateXMLSubtopic (newTopic);
                                SetCurrent (newTopic);
                                SetCurrentXml (CurrentTopic.GUID);
                                PlaceOnSide (newTopic);
                                newTopics.Add (newTopic);
                                UpdateToTop (newTopic);
                                NotifyObservers ();
                        }
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
                                currentIndex = CurrentTopic.Parent.SubtopicList.IndexOf (CurrentTopic);
                        }
                        else
                                return;

                        Topic tempParent = this.CurrentTopic.Parent;
                        Topic deletedTopic = CurrentTopic;

                        deletedTopicPath = deletedTopic.Path;
                        deletedTopics.Add (deletedTopic);

                        currentXmlParent = FindXmlByGuid (CurrentTopic.Parent.GUID);
                        currentXmlTopic = FindXmlByGuid (CurrentTopic.GUID);
                        currentXmlParent.RemoveChild (currentXmlTopic);

                        if (CurrentTopic.Parent.SubtopicList.Count == 1) {
                                CurrentTopic.Parent.SubtopicList.Clear ();
                                CurrentTopic = tempParent;
                        }
                        else {
                                if (currentIndex == (CurrentTopic.Parent.SubtopicList.Count - 1)) {
                                        newIndex = currentIndex - 1;
                                }
                                else {
                                        newIndex = currentIndex;
                                }
                                CurrentTopic.Parent.SubtopicList.RemoveAt (currentIndex);
                                Topic tempTopic = tempParent.SubtopicList[newIndex];
                                SetCurrent (tempTopic);
                                SetCurrentXml (CurrentTopic.GUID);
                        }
                        UpdateToTop (tempParent);
                        NotifyObservers ();
                }

                public void SetTitle (string iTitle)
                {
                        CurrentTopic.Text = (iTitle);
                        changedTopics.Add (CurrentTopic);
                        UpdateToTop (CurrentTopic);
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
                        UpdateAllOffsets ();

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
                        if (CurrentTopic != null)
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

                public void CurrentGoForward ()
                {
                        if (CurrentTopic.Next != null)
                                SetCurrent (CurrentTopic.Next);
                        else
                                CurrentGoDown ();
                }

                public void CurrentGoDown ()
                {
                        if (CurrentTopic.HasChildren) {
                                CurrentTopic.IsExpanded = true;
                                SetCurrent (CurrentTopic.SubtopicList.First);
                        }
                }

                public void CurrentGoBack ()
                {
                        if (CurrentTopic.Previous != null)
                                SetCurrent (CurrentTopic.Previous);
                        else
                                CurrentGoUp ();
                }

                public void CurrentGoUp ()
                {
                        if (CurrentTopic.Parent != null)
                                SetCurrent (CurrentTopic.Parent);
                }

                public void SetCurrentXml (string iGuid)
                {
                        string xPath = ("//Topic[@guid='" + iGuid + "']");
                        currentXmlTopic = (XmlElement) xmlModel.SelectSingleNode (xPath);
                }

                public void ExpandTopic (string iGuid, bool isExpanded)
                {
                        Topic ExpandedTopic = FindByGUID (iGuid);
                        ExpandedTopic.IsExpanded = (isExpanded);
                        UpdateToTop (ExpandedTopic);
                        NotifyObservers ();
                }

                public void ChangeTopic (Topic iTopic)
                {
                        CurrentTopic = iTopic;
                        ChangedTopics.Add (CurrentTopic);
                        UpdateToTop (iTopic);
                        NotifyObservers ();
                }

                public XmlDocument XMLModel
                {
                        get { return xmlModel; }
                }

                public static void InvalidateToTop (Topic iTopic)
                {
                        iTopic.Invalidate ();
                        if (iTopic.Parent != null)
                                InvalidateToTop (iTopic.Parent);
                }

                public static void UpdateToTop (Topic iTopic)
                {
                        iTopic.Update ();
                        if (iTopic.Parent != null)
                                UpdateToTop (iTopic.Parent);
                }

                public static void UpdateOffsets (Topic iTopic)
                {
                        foreach (Topic TempTopic in iTopic.SubtopicList) {
                                TempTopic.Offset.Update (TempTopic);
                                TempTopic.Connection.Update (TempTopic);
                                if (TempTopic.IsExpanded)
                                        UpdateOffsets (TempTopic);
                                TempTopic.UpdateLimits ();
                        }
                }

                public void UpdateAllOffsets ()
                {
                        UpdateOffsets (this.CentralTopic);
                        this.CentralTopic.UpdateLimits ();
                }

                public void ClearCurrent ()
                {
                        if (this.CurrentTopic != null) {
                                this.CurrentTopic.IsCurrent = false;
                                this.CurrentTopic = null;
                        }
                        NotifyObservers ();
                }

                public void SetCurrentByCoords (int iX, int iY)
                {
                        setCurrentByCoords (this.CentralTopic, iX, iY);
                }

                void setCurrentByCoords (Topic iTopic, int iX, int iY)
                {
                        if (iTopic.RegionContainsPoint (iX, iY)) {

                                Console.WriteLine ("Topic: " + iTopic.Text + " contains region hit");

                                if (iTopic.ContainsPoint (iX, iY)) {
                                        Console.WriteLine ("Topic: " + iTopic.Text + " contains hit");
                                        SetCurrent (iTopic);
                                }
                                else {
                                        foreach (Topic child in iTopic.SubtopicList)
                                                setCurrentByCoords (child, iX, iY);
                                }
                        }
                        else
                                return;
                }
        }
}