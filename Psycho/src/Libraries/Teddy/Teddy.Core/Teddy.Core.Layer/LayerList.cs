//------10--------20--------30--------40--------50--------60--------70--------80
//
// ILayerList.cs
// 
// Copyright (C) 2008 Piotr Zurek p.zurek@gmail.com
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//


using System;
using System.Collections;
using System.Collections.Generic;

namespace Teddy.Core
{
	public class LayerList<T> : ILayerList<T>
	where T : ILayer
	{
		public LayerList() : base()
		{
			list.Clear();
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