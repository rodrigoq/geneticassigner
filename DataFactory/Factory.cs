using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataFactory {
	public class Factory {
		protected static string[] GetLineArray(string path) {
			string contents = "";
			using(StreamReader sr = new StreamReader(path, Encoding.Default)) {
				contents = sr.ReadToEnd();
			}
			string[] lines = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			List<string> linesClean = new List<string>();
			for(int i = 0;i < lines.Length;i++) {
				string trimmed = lines[i].Trim();
				if(!trimmed.StartsWith("*"))
					linesClean.Add(trimmed);
			}
			return linesClean.ToArray();
		}

	}
}
