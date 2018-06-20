/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008-2018  Rodrigo Queipo <rodrigoq@gmail.com>
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
using System.IO;
using System.Text;

namespace DataFactory
{
	public class Factory
	{
		protected static string[] GetLineArray(string path)
		{
			string contents = string.Empty;
			using (StreamReader sr = new StreamReader(path, Encoding.Default))
				contents = sr.ReadToEnd();

			string[] lines = contents.Split(
				new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			List<string> linesClean = new List<string>();
			for (int i = 0; i < lines.Length; i++)
			{
				string trimmed = lines[i].Trim();
				if (trimmed.StartsWith("*") == false)
					linesClean.Add(trimmed);
			}
			return linesClean.ToArray();
		}
	}
}
