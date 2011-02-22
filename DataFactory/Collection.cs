/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008-2011  Rodrigo Queipo <rodrigoq@gmail.com>
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

namespace DataFactory
{
	public class Collection<T>: ICollection<T> where T: Identifiable
	{

		protected List<T> list = new List<T>();
		protected Dictionary<int, int> hash = new Dictionary<int, int>();

		public Collection() { }

		public void Sort(Comparison<T> comparison)
		{
			list.Sort(comparison);
			RecreateHash();
		}

		public void Sort(IComparer<T> comparer)
		{
			list.Sort(comparer);
			RecreateHash();
		}

		private void RecreateHash()
		{
			hash.Clear();
			for(int i = 0;i < list.Count;i++)
			{
				hash.Add(list[i].Id, i);
			}
		}

		#region ICollection<T> Members

		public T this[int id]
		{
			get
			{
				return list[hash[id]];
			}
		}

		public T[] ToArray()
		{
			return list.ToArray();
		}

		public void Add(T item)
		{
			list.Add(item);
			hash.Add(item.Id, list.Count - 1);
		}

		public void Clear()
		{
			list.Clear();
			hash.Clear();
		}

		public bool Contains(T item)
		{
			return list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			list.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				return list.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public bool Remove(T item)
		{
			if(hash.Remove(item.Id))
			{
				return list.Remove(item);
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}

		#endregion
	}
}
