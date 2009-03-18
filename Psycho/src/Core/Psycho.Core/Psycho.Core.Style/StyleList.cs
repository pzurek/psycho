//------10--------20--------30--------40--------50--------60--------70--------80
//
// StyleList.cs
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

namespace Psycho.Core
{
	public class StyleList<T> : IStyleList<T>
	where T : IStyle
	{
		public StyleList () : base()
		{
		}
		
		private List<T> list;

		public void Add (T item)
		{
			list.Add (item);
		}

		public void Insert (int index, T item)
		{
			list.Insert (index, item);
		}

		void IList<T>.Insert (int index, T item)
		{
			list.Insert (index, item);
		}

		void IList<T>.RemoveAt (int index)
		{
			list.RemoveAt (index);
		}

		int IList<T>.IndexOf (T item)
		{
			return list.IndexOf (item);
		}

		T IList<T>.this[int index] {
			get { return list[index]; }
			set { list[index] = value; }
		}

		public IEnumerator<T> GetEnumerator ()
		{
			return list.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}

		void ICollection<T>.Add (T item)
		{
			list.Add (item);
		}

		public bool Remove (T item)
		{
			lock (this) {
				return list.Remove (item);
			}
		}

		void ICollection<T>.Clear ()
		{
			list.Clear ();
		}

		bool ICollection<T>.Contains (T item)
		{
			return list.Contains (item);
		}

		void ICollection<T>.CopyTo (T [] array, int arrayIndex)
		{
			list.CopyTo (array, arrayIndex);
		}

		public int Count {
			get {
				lock (this) { 
					return list.Count;
				}
			}
		}

		bool ICollection<T>.IsReadOnly {
			get {
				return ((ICollection<T>)list).IsReadOnly; }
		}

	}
}
