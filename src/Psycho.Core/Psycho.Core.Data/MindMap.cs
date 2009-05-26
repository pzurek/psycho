//------10--------20--------30--------40--------50--------60--------70--------80
//
// MindMap.cs
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
using Psycho.Core.Style;


namespace Psycho.Core.Data
{	
	/// <summary>
	/// The main mind map data container.
	/// </summary>
	
	[XmlRoot]
	public class MindMap : IMindMap
	{
		public MindMap()
		{
		}

		[XmlElement] public ITopic CentralTopic { get; set; }
		[XmlElement] public ITopic CurrentTopic { get; set; }
		[XmlElement] public ITopicList<ITopic> FreeTopicList { get; set; }

        public void CreateTopic ()
        {
                if (CurrentTopic.Parent != null) {
                        ITopic newTopic = new Topic();
                        newTopic.Parent = CurrentTopic.Parent;
                        CurrentTopic.Parent.AddSubtopic (newTopic);
                        newTopic.Map = this;
                        SetCurrent (newTopic);
                }
        }

        public void CreateSubtopic ()
        {
                if (CurrentTopic != null) {
                        ITopic newTopic = new Topic();
                        newTopic.Parent = CurrentTopic;
                        CurrentTopic.IsExpanded = true;
                        CurrentTopic.AddSubtopic (newTopic);
                        newTopic.Map = this;
                        SetCurrent (newTopic);
                }
        }

        public void CreateFreeTopic ()
        {
                ITopic newTopic = new Topic();
				FreeTopicList.Add(newTopic);
                SetCurrent (newTopic);
        }

        public void SetCurrent (ITopic topic)
        {
                if (CurrentTopic != null)
                        CurrentTopic.IsCurrent = false;
                CurrentTopic = topic;
                topic.IsCurrent = true;
        }
	}
}
