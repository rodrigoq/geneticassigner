using System;
using System.Collections.Generic;

namespace DataFactory
{
	public class StudentComparer : IComparer<Student>
	{
		public enum ComparisonType
		{
			Name,
			AssignedCourseAndName
		}
		public ComparisonType CompType { get; set; }

		public StudentComparer(ComparisonType comparisonType)
		{
			CompType = comparisonType;
		}

		private int CompareAssignedCourseAndName(Student x, Student y)
		{
			if (x.AssignedCourse == y.AssignedCourse)
			{
				return CompareName(x, y);
			}
			else
			{
				return x.AssignedCourse.CompareTo(y.AssignedCourse);
			}
		}

		private int CompareName(Student x, Student y)
		{
			return x.Name.CompareTo(y.Name);
		}

		public int Compare(Student x, Student y)
		{
			switch (CompType)
			{
				case ComparisonType.Name:
					return CompareName(x, y);
				case ComparisonType.AssignedCourseAndName:
					return CompareAssignedCourseAndName(x, y);
				default:
					throw new Exception("unknown comparsion type.");
			}
		}
	}
}
