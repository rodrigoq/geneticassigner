/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008  Rodrigo Queipo <rodrigoq@gmail.com>
*
*   This program is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   This program is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;

namespace DataFactory {
	public class Collection<T>: ICollection<T> where T: Identifiable {

		protected List<T> list = new List<T>();

		public Collection() { }

		public void Sort(Comparison<T> comparison) {
			list.Sort(comparison);
		}
		public void Sort(IComparer<T> comparer) {
			list.Sort(comparer);
		}

		#region ICollection<T> Members

		public T this[int id] {
			get {
				Predicate<T> findById = delegate(T t) { return t.Id == id; };
				return list.Find(findById);
			}
		}

		public T[] ToArray() {
			return list.ToArray();
		}

		public void Add(T item) {
			list.Add(item);
		}

		public void Clear() {
			list.Clear();
		}

		public bool Contains(T item) {
			return list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex) {
			list.CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return list.Count; }
		}

		public bool IsReadOnly {
			get { return false; }
		}

		public bool Remove(T item) {
			return list.Remove(item);
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator() {
			return list.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return list.GetEnumerator();
		}

		#endregion
	}
}
