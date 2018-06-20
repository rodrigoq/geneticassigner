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

using System.Drawing;
using System.Windows.Forms;
using GeneticAlgorithm;

namespace GeneticAssigner
{
	public class Graph : Control
	{
		public Graph()
		{
			//this.Height = 200;
			//this.Width = 500;
			BackColor = Color.White;
		}

		public void DrawFitness(int generation, double fitness)
		{
			//Pen myPen = new Pen(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)));//Color.Blue);

			double fitmin = 25.8;
			double fitmax = 26.0;

			double y = Height * (fitness - fitmin) / (fitmax - fitmin) - fitmin;
			int x = Width * generation / 2000;
			using (Pen pen = new Pen(Color.Blue))
			{
				using (Graphics graphics = CreateGraphics())
				{
					graphics.DrawRectangle(pen, x, (float)(Height - y), 1.0f, 1.0f);
				}
			}

		}

		public void DrawStudents(int generation, IIndividual individual)
		{
			//Pen myPen = new Pen(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)));//Color.Blue);

			using (Graphics graphics = CreateGraphics())
			{
				int y = Height * individual.Assigned / individual.Students.Count;
				int x = Width * generation / 2000;
				using (Pen pen = new Pen(Color.Red))
				{
					graphics.DrawRectangle(pen, x, Height - y, 1, 1);
				}

				int y1 = Height * individual.NotAssigned / individual.Students.Count;
				int x1 = Width * generation / 2000;

				using (Pen pen1 = new Pen(Color.Brown))
				{
					graphics.DrawRectangle(pen1, x1, Height - y1, 1, 1);
				}
			}

		}

	}
}
