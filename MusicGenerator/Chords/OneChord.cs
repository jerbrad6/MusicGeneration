using System;
using System.Collections.Generic;
using System.Text;
using static MusicGenerator.Note;

namespace MusicGenerator.Chords
{
	public class OneChord : Chord
	{

		public OneChord(NoteLetter key, MajorMinor majorMinor) : base(key, majorMinor)
		{

			if (_majorMinor == MajorMinor.Major)
			{
				_baseNotes = new List<NoteLetter>(3) { key, AddHalfSteps(key, 4), AddHalfSteps(key, 7)};
			}
			else
			{
				_baseNotes = new List<NoteLetter>(3) { key, AddHalfSteps(key, 3), AddHalfSteps(key, 7) };
			}

		}
	}
}
