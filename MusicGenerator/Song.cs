using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MusicGenerator.SongInputOutput;

namespace MusicGenerator
{
    public class Song
    {
		private static SongInterpreterFactory interpreterFactory = new SongInterpreterFactory();
		public IList<Tuple<int, Note>> Notes { get; }
		public int SampleRate { get; }
		public double LengthInSeconds { get; }

		public Song(string filePath)
		{
			var reader = new StreamReader(filePath);
			Notes = new List<Tuple<int, Note>>(100);
			var songInterpreter = interpreterFactory.GetInterpreter(reader.ReadLine());
			SampleRate = songInterpreter.SampleRate();

			ReadSong(reader, songInterpreter);
			LengthInSeconds = Notes.Last().Item1 / (double)SampleRate;
		}

		private void ReadSong(StreamReader reader, ISongInterpreter songInterpreter)
		{
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				var interpretedLine = songInterpreter.ConvertToNotes(line);
				if (interpretedLine != null && interpretedLine.Any())
				{
					foreach (var note in interpretedLine)
					{
						Notes.Add(note);
					}
				}
			}
		}

    }
}
