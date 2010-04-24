using System;
using System.Collections.Generic;
using System.Text;
using DataFactory;

namespace GeneticAssigner
{
	public static class Context
	{

		public static CourseCollection Courses { get; set; }
		public static StudentCollection Students { get; set; }
		
		public static int Places { get; set; }

		public static void InitializeContext(CourseCollection courses, StudentCollection students, int places)
		{
			Places = places;
			Courses = courses;
			Students = students;
		}

	}
}
