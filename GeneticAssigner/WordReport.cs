/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008-2020  Rodrigo Queipo <rodrigoq@gmail.com>
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
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using XmlTools = OpenXmlPowerTools;

namespace GeneticAssigner
{
	public class WordReport : IDisposable
	{
		private WordprocessingDocument document;
		private string fileName;

		public WordReport(string fileName)
		{
			this.fileName = fileName;
			document = WordprocessingDocument.Open(fileName, true);
		}

		public void Replace(string oldValue, string newValue, bool matchCase = false)
		{
			XmlTools.TextReplacer.SearchAndReplace(document,
				oldValue, newValue, matchCase);
		}

		public void AddText(string text, bool center = true, bool bold = true)
		{
			var doc = document.MainDocumentPart.Document;
			doc.Body.Append(CreateText(text, center, bold));
		}

		public void AddTable(string title, string[] header, string[][] data, bool lastBold, bool firstNumber = true)
		{
			var doc = document.MainDocumentPart.Document;

			Table table = new Table();
			TableProperties props = CreateProperties();
			table.AppendChild(props);

			if(string.IsNullOrEmpty(title) == false)
				table.Append(CreateTitle(title, header.Length));

			table.Append(CreateHeader(header));

			for (var i = 0; i < data.Length; i++)
			{
				var tr = new TableRow();
				for (var j = 0; j < data[i].Length; j++)
				{
					if (firstNumber && j == 0)
						tr.Append(CreateCell((i + 1) + "."));

					tr.Append(CreateCell(data[i][j], (j > 0), 
						(lastBold && j == data[i].Length - 1)));
				}
				table.Append(tr);
			}
			doc.Body.Append(table);
			doc.Body.Append(new Paragraph(new Run(new Break())));
		}

		private TableCell CreateCell(string text, bool center = false, bool bold = false)
		{
			var tc = new TableCell();
			tc.TableCellProperties = new TableCellProperties(
				new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center });

			tc.Append(CreateText(text, center, bold));
			return tc;
		}

		private Paragraph CreateText(string text, bool center, bool bold)
		{
			var run = TextWithNewLine(text);
			if (bold)
				run.RunProperties = new RunProperties(new Bold());

			var par = new Paragraph(run);

			if (center)
				par.ParagraphProperties = new ParagraphProperties(
					new Justification() { Val = JustificationValues.Center });

			return par;
		}

		private Run TextWithNewLine(string text)
		{
			var run = new Run();
			var parts = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			for (int i = 0; i < parts.Length - 1; i++)
			{
				run.AppendChild(new Text(parts[i]));
				run.AppendChild(new Break());
			}
			run.AppendChild(new Text(parts[parts.Length - 1]));
			return run;
		}

		private TableRow CreateTitle(string title, int cols)
		{
			var th = new TableRow();
			var cell = CreateCell(title, true, true);
			cell.Append(new TableCellProperties(new GridSpan() { Val = cols }));
			th.Append(cell);
			return th;
		}

		private TableRow CreateHeader(string[] header)
		{
			var th = new TableRow();
			foreach (var cell in header)
				th.Append(CreateCell(cell, true, true));
			return th;
		}

		private TableProperties CreateProperties()
		{
			var simpleBorder = new EnumValue<BorderValues>(BorderValues.Single);
			return new TableProperties(
				new TableBorders(
					new TopBorder { Val = simpleBorder, },
					new BottomBorder { Val = simpleBorder, },
					new LeftBorder { Val = simpleBorder, },
					new RightBorder { Val = simpleBorder, },
					new InsideHorizontalBorder { Val = simpleBorder, },
					new InsideVerticalBorder { Val = simpleBorder, }
				),
				//Center table
				new TableJustification() { Val = TableRowAlignmentValues.Center }
			);
		}

		public void Save()
		{
			document.Save();
		}
		public void Close()
		{
			document.Close();
		}

		public void Dispose()
		{
			if (document != null)
			{
				Save();
				Close();
				document.Dispose();
				document = null;
			}
		}
	}
}
