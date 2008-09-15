using System;
using System.Collections.Generic;
using System.Text;

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
