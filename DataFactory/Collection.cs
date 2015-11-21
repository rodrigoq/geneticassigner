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
	public class Collection<T> : ICollection<T> where T : Identifiable
	{

		protected List<T> List { get; set; }
		protected Dictionary<int, int> Hash { get; set; }

		public Collection()
		{
			List = new List<T>();
			Hash = new Dictionary<int, int>();
		}

		public void Sort(Comparison<T> comparison)
		{
			List.Sort(comparison);
			RecreateHash();
		}

		public void Sort(IComparer<T> comparer)
		{
			List.Sort(comparer);
			RecreateHash();
		}

		private void RecreateHash()
		{
			Hash.Clear();
			for (int i = 0; i < List.Count; i++)
			{
				Hash.Add(List[i].Id, i);
			}
		}

		#region ICollection<T> Members

		public T this[int id]
		{
			get
			{
				return List[Hash[id]];
			}
		}

		public T[] ToArray()
		{
			return List.ToArray();
		}

		public void Add(T item)
		{
			List.Add(item);
			Hash.Add(item.Id, List.Count - 1);
		}

		public void Clear()
		{
			List.Clear();
			Hash.Clear();
		}

		public bool Contains(T item)
		{
			return List.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			List.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				return List.Count;
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
			if (Hash.Remove(item.Id))
			{
				return List.Remove(item);
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
			return List.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return List.GetEnumerator();
		}

		#endregion
	}
}
