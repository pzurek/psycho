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
		private TopicList<ITopic> subtopicList;
		
		public Topic()
		{
			this.Text = "Topic ";
		}
		
		[XmlElement] public string Text { get; set;}
		[XmlElement] public string StyleID { get; set;}
		[XmlElement] public Note Note  { get; set;}
		[XmlElement] public bool IsExpanded { get; set;}

		public Topic Parent { get; set;}
		public int TotalCount { get; set;}
		public string Path { get; set;}
		public string Number { get; set;}
		public int Level { get; set;}
		
		public bool HasNote {
			get {
				return (Note != null &&
				        !string.IsNullOrEmpty(Note.Text));
			}
		}

		[XmlElement ("Subtopics")]
		public TopicList<ITopic> SubtopicList {
			get {
				return subtopicList;
			}
		}

		public void AddSubtopic ()
		{
			ITopic newTopic = new Topic();
			this.SubtopicList.Add (newTopic);
		}

		public void InsertSubtopic (int index, ITopic item)
		{
			this.SubtopicList.Insert (index, item);
		}

		public void Delete ()
		{
			if (this.Parent == null)
				return;
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
