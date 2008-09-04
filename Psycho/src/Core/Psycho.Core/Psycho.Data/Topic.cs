//------10--------20--------30--------40--------50--------60--------70--------80
//
// Topic.cs
// 
// Copyright (C) 2008 Piotr Zurek p.zurek@gmail.com
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Psycho.Core;

namespace Psycho.Core
{
	public class Topic : MapItem, ITopic
	{
		private string text;
		private string id;
		private Topic parent;
		private Note note;
		private bool isExpanded;
		private TopicList<ITopic> subtopicList;
		private int totalCount;
		private string styleID;
		private string path;
		private string number;
		private int level;
		
		public Topic()
		{
			this.Text = "Topic ";
		}
		
		[XmlElement ()]
		public string Text {
			get { return text; }
			set { text = value; }
		}

		public Topic Parent {
			get { return parent; }
			set { parent = value; }
		}

		[XmlElement ()]
		public Note Note {
			get { return note; }
			set { note = value; }
		}

		[XmlElement ()]
		public bool IsExpanded {
			get { return isExpanded; }
			set { isExpanded = value; }
		}

		public bool HasNote {
			get {
				return (note != null &&
				        !string.IsNullOrEmpty(note.Text));
			}
		}

		public int TotalCount {
			get { return totalCount; }
		}

		public string Path {
			get { return path; }
		}

		public string Number {
			get { return number; }
		}

		public int Level {
			get { return level; }
		}

		[XmlElement ("Subtopics")]
		public TopicList<ITopic> SubtopicList {
			get {
				return subtopicList;
			}
		}

		[XmlElement]
		public string StyleID {
			get { return styleID; }
			set { styleID = value; }
		}

		public void AddSubtopic ()
		{
			ITopic topic = new Topic();
			this.SubtopicList.Add (topic);
		}

		public void InsertSubtopic (int index, ITopic item)
		{
			this.SubtopicList.Insert (index, item);
		}

		public void Delete ()
		{
			if (this.Parent != null)
				this.Parent.SubtopicList.Remove(this);
		}

		public void ForEach (System.Action<ITopic> action)
		{
		}

		public void Update ()
		{
		}
	}
}
