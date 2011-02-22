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
            this.BackColor = Color.White;
        }

        public void DrawFitness(int generation, double fitness)
        {
            //Pen myPen = new Pen(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)));//Color.Blue);

            double fitmin = 25.8;
            double fitmax = 26.0;

            double y = ((double)this.Height * (fitness - fitmin) / (fitmax - fitmin)) - fitmin;
            int x = this.Width * generation / 2000;
            Pen pen = new Pen(Color.Blue);

            Graphics graphics = this.CreateGraphics();
            graphics.DrawRectangle(pen, (float)x, (float)((double)this.Height - y), 1.0f, 1.0f);
            pen.Dispose();
            graphics.Dispose();

        }

        public void DrawStudents(int generation, IIndividual individual)
        {
            //Pen myPen = new Pen(Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)));//Color.Blue);

            int y = this.Height * individual.Assigned / individual.Students.Count;
            int x = this.Width * generation / 2000;
            Pen pen = new Pen(Color.Red);

            Graphics graphics = this.CreateGraphics();
            graphics.DrawRectangle(pen, x, this.Height - y, 1, 1);
            pen.Dispose();

            int y1 = this.Height * individual.NotAssigned / individual.Students.Count;
            int x1 = this.Width * generation / 2000;
            Pen pen1 = new Pen(Color.Brown);

            graphics.DrawRectangle(pen1, x1, this.Height - y1, 1, 1);


            pen1.Dispose();

            graphics.Dispose();

        }

    }
}
