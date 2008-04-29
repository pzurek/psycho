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
namespace Psycho.Core
{
	public class Topic : ITopic
	{
		string ITopic.Text {
			get {
			}
			set {
			}
		}

		string ITopic.GUID {
			get {
			}
		}

		Topic ITopic.Parent {
			get {
			}
			set {
			}
		}

		Note ITopic.Note {
			get {
			}
			set {
			}
		}

		bool ITopic.IsExpanded {
			get {
			}
			set {
			}
		}

		bool ITopic.HasNote {
			get {
			}
		}

		int ITopic.TotalCount {
			get {
			}
		}

		string ITopic.Path {
			get {
			}
		}

		string ITopic.Number {
			get {
			}
		}

		int ITopic.Level {
			get {
			}
		}

		public Topic()
		{
		}

		void ITopic.AddSubtopic (Topic iTopic)
		{
		}

		void ITopic.AddSubtopic (int iIndex, Topic iTopic)
		{
		}

		void ITopic.Delete ()
		{
		}

		void ITopic.ForEach (System.Action<Topic> action)
		{
		}
	}
}
