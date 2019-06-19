using System;
using System.Collections.Generic;
using System.Text;
using static MusicGenerator.Note;

namespace MusicGenerator.Chords
{
    class FiveChord : Chord
    {
		public FiveChord(NoteLetter key, MajorMinor majorMinor) : base(key, majorMinor)
		{

			if (_majorMinor == MajorMinor.Major)
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 7), AddHalfSteps(key, 11), AddHalfSteps(key, 14) };
			}
			else
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 7), AddHalfSteps(key, 10), AddHalfSteps(key, 14) };
			}

		}
	}
}
