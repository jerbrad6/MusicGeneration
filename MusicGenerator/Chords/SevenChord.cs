using System;
using System.Collections.Generic;
using System.Text;
using static MusicGenerator.Note;

namespace MusicGenerator.Chords
{
    public class SevenChord : Chord
    {
		public SevenChord(NoteLetter key, MajorMinor majorMinor) : base(key, majorMinor)
		{

			if (_majorMinor == MajorMinor.Major)
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 11), AddHalfSteps(key, 14), AddHalfSteps(key, 17) };
			}
			else
			{
				_baseNotes = new List<NoteLetter>(3) { AddHalfSteps(key, 11), AddHalfSteps(key, 15), AddHalfSteps(key, 18) };
			}

		}
	}
}
