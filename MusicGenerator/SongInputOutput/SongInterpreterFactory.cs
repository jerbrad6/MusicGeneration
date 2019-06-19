using System;
using System.Collections.Generic;
using System.Text;
using MusicGenerator.Chords;
using static MusicGenerator.Note;

namespace MusicGenerator.SongInputOutput
{
    public class SongInterpreterFactory
    {
		public ISongInterpreter GetInterpreter(string str)
		{
			var args = str.Split(' ');
			switch(args[0].ToUpperInvariant())
			{
				case SongStorageTypes.DefaultStorageType:
					return new DefaultSongInterpreter(Int32.Parse(args[1]), Int32.Parse(args[2]), Int32.Parse(args[3]));
				case SongStorageTypes.ChordStorageType:
					return new ChordSongInterpreter((NoteLetter)Enum.Parse(typeof(NoteLetter), args[1]), (MajorMinor)Enum.Parse(typeof(MajorMinor), args[2]), Int32.Parse(args[3]), Int32.Parse(args[4]), Int32.Parse(args[5]));
				default:
					throw new ArgumentOutOfRangeException($"Unknown Song Storage Type {args[0]}");
			}
		}
    }
}
